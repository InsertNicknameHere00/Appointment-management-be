using AppointmentAPI.Entities;
using AppointmentAPI.Entities.Enums;
using AppointmentAPI.Repository;
using AppointmentAPI.Repository.Interfaces;
using AppointmentAPI.Services.Interfaces;
using MailKit.Search;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Org.BouncyCastle.Asn1.X509;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppointmentAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailSendService _emailSendService;
        private readonly IUsersServiceRepository usersServiceRepository;

        public OrderService(IOrderRepository orderService, IProductRepository productRepository = null, IEmailSendService emailSendService = null, IUsersServiceRepository _usersServiceRepository = null)
        {
            _orderRepository = orderService;
            _productRepository = productRepository;
            _emailSendService = emailSendService;
            usersServiceRepository = _usersServiceRepository;
        }
        public async Task<Order> CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems, string address)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                OrderAddress = address,
                OrderStatus = Entities.Enums.OrderStatus.Pending,
                PromoCodeID = null
            };
            await _orderRepository.AddOrder(order);

            foreach (var cartItem in cartItems)
            {

                var product = await _productRepository.GetProductById(cartItem.Product.ProductId);
                if (product != null)
                {
                    var item = new OrderItem();
                    item.OrderId = order.OrderId;
                    item.ProductId = product.ProductId;
                    item.Price = product.Price;
                    item.Quantity = cartItem.Quantity;
                    await _orderRepository.AddOrderItem(item);

                    product.Quantity -= cartItem.Quantity;
                    await _productRepository.UpdateProduct(product.ProductId, product);
                }
            }

            return order;

        }

        public async Task<Order> UpdateOrder(int orderId, Order _order)
        {
            var result = await _orderRepository.GetOrderById(orderId);
            if (_order != null)
            {
                result.OrderId = _order.OrderId;
                result.OrderDate = _order.OrderDate;
                result.OrderAddress = _order.OrderAddress;
                result.OrderStatus = _order.OrderStatus;
                await _orderRepository.UpdateOrder(result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<Product> CheckQuantity(IEnumerable<CartItem> cartItems)
        {
            var result = new List<Product>();
            foreach (var item in cartItems)
            {
                var product = _productRepository.Search(item.Product.ProductId);
                if (product.Quantity < item.Quantity)
                {
                    result.Add(product);
                }

            }
            return result;

        }

        public async Task<Order> CancelOrder(int orderId, Order order)
        {
            var result = await _orderRepository.GetOrderById(orderId);
            if (order != null)
            {
                result.OrderId = order.OrderId;
                result.OrderDate = order.OrderDate;
                result.OrderAddress = order.OrderAddress;
                result.OrderStatus = OrderStatus.Cancelled;
                await _orderRepository.UpdateOrder(result);

                var orderedItems = _orderRepository.GetOrderItemsByOrderId(orderId);

                foreach (var product in orderedItems)
                {
                    var currentProduct = await _productRepository.GetProductById(product.ProductId);
                    currentProduct.Quantity += product.Quantity;
                    await _productRepository.UpdateProduct(currentProduct.ProductId, currentProduct);
                }
                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task SendEmail(Order order)
        {
            var request = new EmailRequest();
            string subject = "Поръчка номер " + order.OrderId;
            Users user = await usersServiceRepository.GetUsersByID(order.UserId);
            string email = user.Email;
            StringBuilder body = new StringBuilder();
            body.Append("Здравей, " + order.User.FirstName + " " + order.User.LastName + ".\n");
            body.Append("Ние, от AppointmentAPI, благодарим, че избрахте нас. ");
            body.Append("Статусът на вашата поръчка е: " + order.OrderStatus + ". ");
            body.Append("По- долу можете да намерите още информация за поръчката. \n");
            body.Append("Дата на създаване на поръчка: " + order.OrderDate+"\n");
            body.Append("Адрес за доставка: " + order.OrderAddress+"\n");
            body.Append("Цена: " + order.TotalPrice.ToString("F2") + "лв.\n");
            body.Append("Продукти: \n");          
            
            var orderedItems = _orderRepository.GetOrderItemsByOrderId(order.OrderId);
            foreach (var item in orderedItems)
            {
                var currentProduct = await _productRepository.GetProductById(item.ProductId);
                body.Append(currentProduct.ProductName + " - " + item.Quantity + " бр.\n");
            }
            request.Subject = subject;
            request.Body = body.ToString();
            request.ToEmail = order.User.Email;
            Console.WriteLine("Sending...");
            await _emailSendService.SendEmail(request);

        }

    }
}

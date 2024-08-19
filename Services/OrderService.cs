using AppointmentAPI.Entities;
using AppointmentAPI.Entities.Enums;
using AppointmentAPI.Repository.Interfaces;
using AppointmentAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace AppointmentAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderService, IProductRepository productRepository = null)
        {
            _orderRepository = orderService;
            _productRepository = productRepository;
        }
        public async Task<Order> CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems,string address)
        {
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = cartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                    OrderAddress = address,
                    OrderStatus = Entities.Enums.OrderStatus.Pending
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

        public async Task<Order> UpdateOrder(int orderId,Order _order)
        {
            var result = await _orderRepository.GetOrderById(orderId);
            if (_order != null)
            {
                Enum.TryParse(typeof(OrderStatus), result.OrderStatus.ToString(), out object? statusResult);
                if (statusResult == null)
                {
                    throw new DbUpdateException();
                }

                result.OrderId = _order.OrderId;
                result.OrderDate = _order.OrderDate;
                result.OrderAddress=_order.OrderAddress;
                result.OrderStatus = (OrderStatus)statusResult!;
                await _orderRepository.UpdateOrder(result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public string CheckQuantity(IEnumerable<CartItem> cartItems)
        {
            var result = "";
            foreach(var item in cartItems)
            {
                var product = _productRepository.Search(item.Product.ProductId);
                if (product.Quantity <item.Quantity)
                { 
                    result= item.Product.ProductName;
                }
               
            }
            return result;
            
        }
    }
}

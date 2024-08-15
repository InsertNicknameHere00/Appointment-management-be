using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Order> CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                OrderAddress = "Plovidv"
                /*OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.Product.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToList()*/

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
                    await _productRepository.UpdateProduct(product.ProductId,product);
                }
            }

            return order;
        }
    }
}

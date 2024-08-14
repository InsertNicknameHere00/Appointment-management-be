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
        public async Task CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.Product.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToList()
            };

            foreach (var cartItem in cartItems)
            {
                var product = await _productRepository.GetProductById(cartItem.Product.ProductId);
                if (product != null)
                {
                    product.Quantity -= cartItem.Quantity;
                }
            }

            await _orderRepository.AddOrder(order);
        }
    }
}

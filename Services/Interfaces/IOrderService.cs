using AppointmentAPI.Entities;
using System.Threading.Tasks;

namespace AppointmentAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems, string address);
        Task<Order> UpdateOrder(int orderId, Order _order);
        IEnumerable<Product> CheckQuantity(IEnumerable<CartItem> cartItems);
        Task<Order> CancelOrder (int orderId, Order order);
        Task SendEmail(Order order);
    }
}

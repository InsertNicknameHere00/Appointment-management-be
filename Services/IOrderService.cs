using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems);
    }
}

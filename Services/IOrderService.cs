using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems);
    }
}

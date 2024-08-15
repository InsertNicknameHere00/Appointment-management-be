using AppointmentAPI.Entities;
using System.Threading.Tasks;

namespace AppointmentAPI.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int userId, IEnumerable<CartItem> cartItems,string address);
        Task<Order> UpdateOrder(int orderId, Order _order);
        string CheckQuantity(IEnumerable<CartItem> cartItems);
    }
}

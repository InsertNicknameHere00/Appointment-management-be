using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<OrderItem> AddOrderItem(OrderItem item);
        Task<Order> UpdateOrder(Order order);
        Task<Order> GetOrderById(int id);

    }
}

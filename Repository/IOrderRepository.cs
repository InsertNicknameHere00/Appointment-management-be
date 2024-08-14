using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);

    }
}

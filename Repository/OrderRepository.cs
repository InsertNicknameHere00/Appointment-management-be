using AppointmentAPI.Data;
using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HaircutSalonDbContext _context;
        public OrderRepository(HaircutSalonDbContext context)
        {
            _context = context;

        }
        public async Task<Order> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public Task<Order> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

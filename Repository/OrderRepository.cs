using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository.Interfaces;
using Microsoft.CodeAnalysis;

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

        public async Task<OrderItem> AddOrderItem(OrderItem item)
        {
            await _context.OrderItem.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
           return _context.OrderItem.Where(o => o.OrderId == orderId).ToList();
        }

        
    }
}

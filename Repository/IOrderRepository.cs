﻿using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<OrderItem> AddOrderItem(OrderItem item);
        Task<Order> UpdateOrder(Order order);

    }
}

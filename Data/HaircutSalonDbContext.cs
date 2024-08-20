using System;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;


namespace AppointmentAPI.Data
{
    public class HaircutSalonDbContext : DbContext
    {
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<SalonService> SalonService { get; set; }
        public DbSet<AdminService> AdminServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<PromoCodes> PromoCodes { get; set; }   
    }
}


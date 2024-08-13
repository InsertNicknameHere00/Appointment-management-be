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
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }
    }
}


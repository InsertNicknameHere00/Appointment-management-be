using System;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Data
{
    public class HaircutSalonDbContext : DbContext
    {

        public DbSet<SalonService> SalonService { get; set; }
        public DbSet<AdminService> AdminService { get; set; }
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }
    }
}


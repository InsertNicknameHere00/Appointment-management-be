using System;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Data
{
    public class HaircutSalonDbContext : DbContext
    {
        
        public DbSet<Users> Users { get; set; }
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }
    }
}


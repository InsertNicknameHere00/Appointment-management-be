using System;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Data
{
    public class HaircutSalonDbContext : DbContext
    {
        
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }
    }
}


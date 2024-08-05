using System;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace AppointmentAPI.Data
{
    public class HaircutSalonDbContext : DbContext
    {
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}


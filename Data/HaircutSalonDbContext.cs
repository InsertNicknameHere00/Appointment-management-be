﻿using System;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace AppointmentAPI.Data
{
    public class HaircutSalonDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<SalonService> SalonService { get; set; }
        public DbSet<AdminService> AdminServices { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public HaircutSalonDbContext(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }
    }
}


﻿namespace AppointmentAPI.Repositories
{
    using AppointmentAPI.Data;
    using AppointmentAPI.Entities;
    using AppointmentAPI.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HaircutSalonDbContext dbContext;

        public AppointmentRepository(HaircutSalonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Appointment> Add(Appointment appointment)
        {
            var tempAppointment = await this.dbContext.Appointments.AddAsync(appointment);

            await this.dbContext.SaveChangesAsync();

            return tempAppointment.Entity;
        }

        public async Task Update(Appointment appointment)
        {
            this.dbContext.Appointments.Update(appointment);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(Appointment appointment)
        {
            this.dbContext.Appointments.Remove(appointment);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id)
        {
            bool result = await this.dbContext.Appointments
                                     .AnyAsync(a => a.Id == id);

            return result;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await this.dbContext.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await this.dbContext.Appointments
                                    .FirstAsync(a => a.Id == id);
        }

        public async Task<bool> IsUserOwner(int id, int userId)
        {
            Appointment tempAppointment = await this.dbContext.Appointments
                                                    .FirstAsync(a => a.Id == id);

            return tempAppointment.UserId == userId;
        }

        public async Task BookAnAppointment(int id, int clientId)
        {
            Appointment tempAppointment = await this.GetById(id);
            tempAppointment.ClientId = clientId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsBookedAsync(int id)
        {
            Appointment tempAppointment = await this.GetById(id);

            return tempAppointment.ClientId.HasValue;
        }

        public async Task<bool> IsBookedByUserWithId(int id, int clientId)
        {
            Appointment tempAppointment = await this.GetById(id);

            return tempAppointment.ClientId.HasValue && tempAppointment.ClientId == clientId;
        }

        public async Task CancelAnAppointment(int id)
        {
            Appointment tempAppointment = await this.GetById(id);
            tempAppointment.ClientId = null;

            await this.dbContext.SaveChangesAsync();
        }
    }
}

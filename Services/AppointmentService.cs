namespace AppointmentAPI.Services
{
    using AppointmentAPI.Data;
    using AppointmentAPI.Entities;
    using AppointmentAPI.Entities.Enums;
    using AppointmentAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentService : IAppointmentService
    {
        private readonly HaircutSalonDbContext dbContext;

        public AppointmentService(HaircutSalonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await this.dbContext.Appointments.ToListAsync();
        }


        public async Task<int> CreateAsync(int userId)
        {
            Appointment appointment = new Appointment() 
            { 
                Status = StatusType.Available,
                StartDate = DateTime.Now,
                // TODO: get duration from service and add it to endDate
                EndDate = DateTime.Now.AddMinutes(30),
                UserId = userId,
            };

            await this.dbContext.Appointments.AddAsync(appointment);
            await this.dbContext.SaveChangesAsync();

            return appointment.Id;
        }

        public async Task DeleteAsync(int id)
        {
            Appointment appointment = await this.GetById(id);

            this.dbContext.Appointments.Remove(appointment);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> EditAsync(int id, int userId, string status)
        {
            Appointment appointment = await this.GetById(id);
            Enum.TryParse(typeof(StatusType), status.ToString(), out object? statusResult);

            appointment.StartDate = DateTime.Now;
            appointment.EndDate = DateTime.Now.AddMinutes(30);
            appointment.Status = (StatusType)statusResult!;
            appointment.UserId = userId;

            await this.dbContext.SaveChangesAsync();

            return appointment.Id;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext.Appointments
                                    .AnyAsync(a => a.Id == id);

            return result;
        }

        public async Task<Appointment> GetById(int id)
            => await this.dbContext.Appointments.FirstAsync(a => a.Id == id);

        public async Task<bool> IsUserOwner(int id, int userId)
        {
            Appointment tempAppointment = await this.dbContext.Appointments
                                                    .FirstAsync(a => a.Id == id);

            return tempAppointment.UserId == userId;
        }
    }
}

namespace AppointmentAPI.Services
{
    using AppointmentAPI.Data;
    using AppointmentAPI.Entities;
    using AppointmentAPI.Entities.Enums;
    using AppointmentAPI.Repositories.Interfaces;
    using AppointmentAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await repository.GetAll();
        }


        public async Task<Appointment> CreateAsync(int userId)
        {
            Appointment appointment = new Appointment() 
            { 
                Status = StatusType.Available,
                StartDate = DateTime.Now,
                // TODO: get duration from service and add it to endDate
                EndDate = DateTime.Now.AddMinutes(30),
                UserId = userId,
            };

            var result = await this.repository.Add(appointment);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            Appointment appointment = await this.GetById(id);

            await this.repository.Delete(appointment);
        }

        public async Task<int> EditAsync(int id, int userId, string status)
        {
            Appointment appointment = await this.GetById(id);
            Enum.TryParse(typeof(StatusType), status.ToString(), out object? statusResult);

            appointment.StartDate = DateTime.Now;
            appointment.EndDate = DateTime.Now.AddMinutes(30);
            appointment.Status = (StatusType)statusResult!;
            appointment.UserId = userId;

            await this.repository.Update(appointment);

            return appointment.Id;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await this.repository.ExistsByIdAsync(id);
        }

        public async Task<Appointment> GetById(int id)
            => await this.repository.GetById(id);

        public async Task<bool> IsUserOwner(int id, int userId)
        {
            return await this.repository.IsUserOwner(id, userId);
        }
    }
}

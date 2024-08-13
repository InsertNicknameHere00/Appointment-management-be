namespace AppointmentAPI.Services
{
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
            var result = await repository.GetAll();
            if (result.Any()) 
            { 
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }


        public async Task<Appointment> CreateAsync(int userId, int serviceId)
        {
            var appointment = new Appointment() 
            { 
                Status = StatusType.Available,
                StartDate = DateTime.Now,
                // TODO: get duration from service and add it to endDate
                EndDate = DateTime.Now.AddMinutes(30),
                UserId = userId,
                ServiceId = serviceId
            };

            var result = await this.repository.Add(appointment);
            if (result != null)
            {
                return result;
            } 
            else 
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await this.GetById(id);
            if (appointment != null)
            {
                await this.repository.Delete(appointment);
            }
            else 
            {
                throw new KeyNotFoundException();
            }

        }

        public async Task<Appointment> EditAsync(int id, int userId, string status)
        {
            var appointment = await this.GetById(id);
            if (appointment != null)
            {
                Enum.TryParse(typeof(StatusType), status.ToString(), out object? statusResult);
                if (statusResult == null) 
                {
                    throw new DbUpdateException();
                }

                appointment.StartDate = DateTime.Now;
                appointment.EndDate = DateTime.Now.AddMinutes(30);
                appointment.Status = (StatusType)statusResult!;
                appointment.UserId = userId;

                await this.repository.Update(appointment);

                return appointment;
            }
            else 
            {
                throw new KeyNotFoundException();
            }
            
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await this.repository.ExistsById(id);
        }

        public async Task<Appointment> GetById(int id)
        {
            var result = await this.repository.GetById(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<bool> IsUserOwner(int id, int userId)
        {
            return await this.repository.IsUserOwner(id, userId);
        }

        public async Task BookAnAppointment(int id, int clientId)
        {
            var appointment = await this.GetById(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException();
            }
           
            var isAppointmentBooked = await this.repository.IsBookedAsync(id);
            if (isAppointmentBooked) 
            {
                throw new Exception("Appointment is already booked");
            }

            await this.repository.BookAnAppointment(id, clientId);
        }

        public async Task CancelAnAppointment(int id, int clientId)
        {
            var appointment = await this.GetById(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException();
            }

            var isAppointmentBookedByClient = await this.repository.IsBookedByUserWithId(id, clientId);
            if (!isAppointmentBookedByClient)
            {
                throw new Exception("Appointment is already booked by another client");
            }

            await this.repository.CancelAnAppointment(id);
        }
    }
}

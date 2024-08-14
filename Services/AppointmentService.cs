namespace AppointmentAPI.Services
{
    using AppointmentAPI.Entities;
    using AppointmentAPI.Entities.Enums;
    using AppointmentAPI.Repository;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository repository;
        private readonly ISalonServiceRepository salonServiceRepository;
        private readonly IUsersServiceRepository usersServiceRepository;

        public AppointmentService(IAppointmentRepository repository, ISalonServiceRepository salonServiceRepository, IUsersServiceRepository usersServiceRepository)
        {
            this.repository = repository;
            this.salonServiceRepository = salonServiceRepository;
            this.usersServiceRepository = usersServiceRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            var result = await this.repository.GetAll();
            if (result.Any()) 
            {
                foreach (var item in result)
                {
                    MapServiceAndUserToEntity(item);
                }

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

            MapServiceAndUserToEntity(appointment);

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
                MapServiceAndUserToEntity(appointment);

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
                MapServiceAndUserToEntity(result);

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

        private void MapServiceAndUserToEntity(Appointment appointment) 
        {
            var service = this.salonServiceRepository.Search(appointment.ServiceId);
            appointment.Service = service;
            var user = this.usersServiceRepository.GetUserByID(appointment.UserId);
            appointment.User = user;
        }  
    }
}

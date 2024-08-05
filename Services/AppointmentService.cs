namespace AppointmentAPI.Services
{
    using AppointmentAPI.Data;
    using AppointmentAPI.Entities;
    using AppointmentAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AppointmentService : IAppointmentService
    {
        private readonly HaircutSalonDbContext dbContext;

        public AppointmentService(HaircutSalonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> CreateAsync(string id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserOwner(string id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

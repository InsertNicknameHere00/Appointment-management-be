namespace AppointmentAPI.Repositories
{
    using AppointmentAPI.Entities;
    using AppointmentAPI.Repositories.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AppointmentRepository : IAppointmentRepository
    {
        public Task<Appointment> Add(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserOwner(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}

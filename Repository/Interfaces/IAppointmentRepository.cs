namespace AppointmentAPI.Repository.Interfaces
{
    using AppointmentAPI.Entities;

    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();

        Task<Appointment> GetById(int id);

        Task<Appointment> Add(Appointment appointment);

        Task Update(Appointment appointment);

        Task Delete(Appointment appointment);

        Task<bool> ExistsById(int id);

        Task<bool> IsUserOwner(int id, int userId);

        Task BookAnAppointment(int id, int clientId);

        Task CancelAnAppointment(int id);

        Task<bool> IsBookedAsync(int id);

        Task<bool> IsBookedByUserWithId(int id, int clientId);
    }
}

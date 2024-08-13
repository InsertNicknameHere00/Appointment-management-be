namespace AppointmentAPI.Services.Interfaces
{
    using AppointmentAPI.Entities;

    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();

        Task<Appointment> CreateAsync(int userId, int serviceId);

        Task<Appointment> EditAsync(int id, int userId, string status);

        Task DeleteAsync(int id);

        Task<bool> ExistsByIdAsync(int id);

        Task<Appointment> GetById(int id);

        Task<bool> IsUserOwner(int id, int userId);

        Task BookAnAppointment(int id, int clientId);

        Task CancelAnAppointment(int id, int clientId);
    }
}

namespace AppointmentAPI.Services.Interfaces
{
    using AppointmentAPI.Entities;
    using Microsoft.Extensions.Hosting;

    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll();

        Task<Appointment> CreateAsync(int userId);

        Task<int> EditAsync(int id, int userId, string status);

        Task DeleteAsync(int id);

        Task<bool> ExistsByIdAsync(int id);

        Task<Appointment> GetById(int id);

        Task<bool> IsUserOwner(int id, int userId);
    }
}

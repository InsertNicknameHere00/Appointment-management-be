namespace AppointmentAPI.Services.Interfaces
{
    using AppointmentAPI.Entities;
    using Microsoft.Extensions.Hosting;

    public interface IAppointmentService
    {
        Task<int> CreateAsync(string id, string userId);

        Task EditAsync(int id, string userId);

        Task DeleteAsync(int id);

        Task<bool> ExistsByIdAsync(int id);

        Task<Appointment> GetById(int id);

        Task<bool> IsUserOwner(string id, string userId);
    }
}

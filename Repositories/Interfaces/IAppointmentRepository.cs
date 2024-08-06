namespace AppointmentAPI.Repositories.Interfaces
{
    using AppointmentAPI.Entities;

    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();

        Task<Appointment> GetById(int id);

        Task<Appointment> Add(Appointment appointment);

        Task Update(Appointment appointment);

        Task Delete(Appointment appointment);

        Task<bool> ExistsByIdAsync(int id);

        Task<bool> IsUserOwner(int id, int userId);
    }
}

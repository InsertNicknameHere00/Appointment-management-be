namespace AppointmentAPI.Repositories.Interfaces
{
    using AppointmentAPI.Entities;

    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();

        Task<Appointment> GetById(int id);

        Task<Appointment> Add(Appointment appointment);

        Task<Appointment> Update(Appointment appointment);

        Task<Appointment> Delete(int id);

        Task<bool> ExistsByIdAsync(int id);

        Task<bool> IsUserOwner(int id, int userId);
    }
}

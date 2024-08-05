using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface ISalonServiceRepository
    {
        Task<List<SalonService>> GetAllSalonServices();
        Task GetSalonServicesById(int serviceId);
        Task AddSalonService(SalonService salonService);
        Task UpdateSalonService(int serviceId,SalonService salonService);
        Task DeleteSalonService(int id);
        //Task GetSalonServiceByTitle(string title);
    }
}

using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface ISalonServiceRepository
    {
        Task<List<SalonService>> GetAllSalonServices();
        Task<SalonService> GetSalonServicesById(int serviceId);
        Task<SalonService> AddSalonService(SalonService salonService);
        Task<SalonService> UpdateSalonService(int serviceId,SalonService salonService);
        Task<bool> DeleteSalonService(int id);
        //Task GetSalonServiceByTitle(string title);
    }
}

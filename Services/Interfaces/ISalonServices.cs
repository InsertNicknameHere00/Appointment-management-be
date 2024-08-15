using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface ISalonServices
    {
        Task<List<SalonService>> GetAllSalonServices();
        Task<SalonService> GetSalonServiceById(int id);
        Task<SalonService> Save(SalonService salonService);

        Task<SalonService> Update(int serviceId, SalonService salonService);
        Task<bool> Delete(int id);
    }
}

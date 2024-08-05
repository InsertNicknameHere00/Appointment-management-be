using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface ISalonServices
    {
        Task<List<SalonService>> GetAll();
        SalonService GetById(int id);
        bool Save(SalonService salonService);

        bool Update(SalonService salonService);
        bool Delete(int id);
    }
}

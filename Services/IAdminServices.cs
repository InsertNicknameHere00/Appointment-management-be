using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IAdminServices
    {
        Task<List<AdminService>> GetAll();
        AdminService GetById(int id);
        Task<List<AdminService>> GetByServiceId(int id);
        Task<List<AdminService>> GetByAdminId(int id);
        bool Save(AdminService adminService);

        bool Update(AdminService adminService);
        bool Delete(int id);
    }
}

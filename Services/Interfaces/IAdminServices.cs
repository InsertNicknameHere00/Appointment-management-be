using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IAdminServices
    {
        Task<List<AdminService>> GetAllAdminServices();
        Task<AdminService> GetAdminServicesById(int id);
        Task<List<AdminService>> GetAdminServiceByServiceId(int id);
        Task<List<AdminService>> GetAdminServiceByAdminId(int id);
        Task<AdminService> Save(AdminService adminService);

        Task<AdminService> Update(int id, AdminService adminService);
        Task<bool> Delete(int id);
    }
}

using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IAdminServiceRepository
    {

        Task<List<AdminService>> GetAllAdminServices();
       Task <List<AdminService>> GetAdminServiceByServiceId(int serviceId);
       Task <List<AdminService>> GetAdminServiceByAdminId(int adminId);
        Task<AdminService> GetAdminServiceById(int id);
        Task<AdminService> AddAdminService(AdminService adminService);
        Task<AdminService> UpdateAdminService(int id, AdminService adminService);
        Task<bool> DeleteAdminService(int id);
    }
}

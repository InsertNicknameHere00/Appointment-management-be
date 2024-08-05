using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IAdminServiceRepository
    {

        Task<List<AdminService>> GetAllAdminServices();
       Task <List<AdminService>> GetAdminServiceByServiceId(int serviceId);
       Task <List<AdminService>> GetAdminServiceByAdminId(int adminId);
        Task GetAdminServiceById(int id);
        Task AddAdminService(AdminService adminService);
        Task UpdateAdminService(int id);
        Task DeleteAdminService(int id);
    }
}

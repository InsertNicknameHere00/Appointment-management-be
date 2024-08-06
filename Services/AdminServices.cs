using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using System.Runtime.InteropServices;

namespace AppointmentAPI.Services
{
    public class AdminServices:IAdminServices
    {
        private readonly IAdminServiceRepository _repository;
        public AdminServices(IAdminServiceRepository adminServiceRepository)
        {
            _repository = adminServiceRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAdminService(id);

        }

        public async Task<List<AdminService>> GetAllAdminServices()
        {
            return await _repository.GetAllAdminServices();
        }

        public async Task<List<AdminService>> GetAdminServiceByAdminId(int id)
        {
            return await _repository.GetAdminServiceByAdminId(id);
        }

        public async Task<AdminService> GetAdminServicesById(int id)
        {
            return await _repository.GetAdminServiceById(id);
        }

        public async Task<List<AdminService>> GetAdminServiceByServiceId(int id)
        {
            return await _repository.GetAdminServiceByServiceId(id);
        }

        public async Task<AdminService> Save(AdminService adminService)
        {
            return await _repository.AddAdminService(adminService);

        }

        public async Task<AdminService> Update(int serviceId, AdminService adminService)
        {
            return await _repository.UpdateAdminService(serviceId, adminService);
        }

    }
}

using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using System.Runtime.InteropServices;

namespace AppointmentAPI.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminServiceRepository _repository;
        public AdminServices(IAdminServiceRepository adminServiceRepository)
        {
            _repository = adminServiceRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var result = _repository.Search(id);
            if (result != null)
            {
                await _repository.DeleteAdminService(result);
                return true;
            }
            else
            {
                return false;
            }
            //return await _repository.DeleteAdminService(id);

        }

        public async Task<List<AdminService>> GetAllAdminServices()
        {
            var result = await _repository.GetAllAdminServices();
            if (result.Count != 0)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.GetAllAdminServices();
        }

        public async Task<List<AdminService>> GetAdminServiceByAdminId(int id)
        {
            var result = await _repository.GetAdminServiceByAdminId(id);
            if (result.Count != 0)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.GetAdminServiceByAdminId(id);
        }

        public async Task<AdminService> GetAdminServicesById(int id)
        {
            var result = await _repository.GetAdminServiceById(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.GetAdminServiceById(id);
        }

        public async Task<List<AdminService>> GetAdminServiceByServiceId(int id)
        {
            var result = await _repository.GetAllAdminServices();
            if (result.Count != 0)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.GetAdminServiceByServiceId(id);
        }

        public async Task<AdminService> Save(AdminService adminService)
        {
            if (adminService != null)
            {
                var result = new AdminService();
                result.AdminServicesId = adminService.AdminServicesId;
                result.ServiceId = adminService.ServiceId;
                result.UserId = adminService.UserId;
                result.ServiceDuration = adminService.ServiceDuration;
                result.ServicePrice = adminService.ServicePrice;
                await _repository.AddAdminService(result);
                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.AddAdminService(adminService);

        }

        public async Task<AdminService> Update(int serviceId, AdminService adminService)
        {
            var result = await _repository.GetAdminServiceById(serviceId);
            if (result != null)
            {
                result.AdminServicesId = serviceId;
                result.ServiceId = adminService.ServiceId;
                result.UserId = adminService.UserId;
                result.ServiceDuration = adminService.ServiceDuration;
                result.ServicePrice = adminService.ServicePrice;
                await _repository.UpdateAdminService(serviceId, result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.UpdateAdminService(serviceId, adminService);
        }

    }
}

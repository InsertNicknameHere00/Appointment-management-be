using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository
{
    public class AdminServiceRepository : IAdminServiceRepository
    {
        private readonly HaircutSalonDbContext _context;
        public AdminServiceRepository(HaircutSalonDbContext context)
        {
            _context = context;

        }
       public async Task<AdminService> AddAdminService(AdminService adminService)
        {
            var currentService = new AdminService();
            currentService.Id = adminService.Id;
            currentService.ServiceId = adminService.ServiceId;
            currentService.UserId = adminService.UserId;
            currentService.ServiceDuration = adminService.ServiceDuration;
            currentService.ServicePrice = adminService.ServicePrice;
            await _context.AdminServices.AddAsync(currentService);
            await _context.SaveChangesAsync();
            return currentService;
        }

        public async Task<bool> DeleteAdminService(int id)
        {
            var service = await _context.AdminServices.FindAsync(id);
            if (service != null)
            {
                _context.AdminServices.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<AdminService>> GetAdminServiceByAdminId(int adminId)
        {
            List<AdminService> services = _context.AdminServices.Where(s => s.UserId == adminId).ToList();
            return services;
        }

        public async Task<AdminService> GetAdminServiceById(int id)
        {
            var service = await _context.AdminServices.FindAsync(id);
            if (service != null)
            {
                return service;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<AdminService>> GetAdminServiceByServiceId(int serviceId)
        {
           List<AdminService> services=_context.AdminServices.Where(s=>s.ServiceId == serviceId).ToList();
            return services;
        }

        public async Task<List<AdminService>> GetAllAdminServices()
        {
            return await _context.AdminServices.ToListAsync();
        }

        public async Task<AdminService> UpdateAdminService(int id,AdminService adminService)
        {
            var currentService = await _context.AdminServices.FindAsync(id);
            if (currentService != null)
            {
                currentService.Id = id;
                currentService.ServiceId = adminService.ServiceId;
                currentService.UserId = adminService.UserId;
                currentService.ServiceDuration = adminService.ServiceDuration;
                currentService.ServicePrice = adminService.ServicePrice;
                _context.AdminServices.Update(currentService);
                await _context.SaveChangesAsync();
                return currentService;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            
        }
    }
}

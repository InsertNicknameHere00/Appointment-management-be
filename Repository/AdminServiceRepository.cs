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
            await _context.AdminServices.AddAsync(adminService);
            await _context.SaveChangesAsync();
            return adminService;
        }

        public async Task<bool> DeleteAdminService(AdminService adminService)
        {
            _context.AdminServices.Remove(adminService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AdminService>> GetAdminServiceByAdminId(int adminId)
        {
            List<AdminService> services = _context.AdminServices.Where(s => s.UserId == adminId).ToList();
            return services;
        }

        public async Task<AdminService> GetAdminServiceById(int id)
        {
            return await _context.AdminServices.FindAsync(id);
           
        }

        public async Task<List<AdminService>> GetAdminServiceByServiceId(int serviceId)
        {
            List<AdminService> services = _context.AdminServices.Where(s => s.ServiceId == serviceId).ToList();
            return services;
        }

        public async Task<List<AdminService>> GetAllAdminServices()
        {
            return await _context.AdminServices.ToListAsync();
        }

        public async Task<AdminService> UpdateAdminService(int id, AdminService adminService)
        {
            
            _context.AdminServices.Update(adminService);
            await _context.SaveChangesAsync();
            return adminService;

        }

        public AdminService Search(int id)
        {
            var service = _context.AdminServices.Find(id);
            return service;

        }
    }
}

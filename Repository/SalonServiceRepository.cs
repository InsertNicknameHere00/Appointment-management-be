using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository
{
    public class SalonServiceRepository : ISalonServiceRepository
    {
        private readonly HaircutSalonDbContext _context;
        public SalonServiceRepository(HaircutSalonDbContext context) { 
            _context = context;
            
        }

        public async Task<SalonService> AddSalonService(SalonService salonService)
        {
            await _context.SalonService.AddAsync(salonService);
            await _context.SaveChangesAsync();
            return salonService;
        }

        public async Task<bool> DeleteSalonService(int id)
        {
            var service = await _context.SalonService.FindAsync(id);
            if (service != null)
            {
                _context.SalonService.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new KeyNotFoundException("Problem with service ID.");
            }
        }

        public async Task<List<SalonService>> GetAllSalonServices()
        {
            return await _context.SalonService.ToListAsync();
        }

        public async Task<SalonService> GetSalonServicesById(int serviceId)
        {
            var service=await _context.SalonService.FindAsync(serviceId);
            if (service != null)
            {
                return service;
            }
            else
            {
                throw new KeyNotFoundException("Service not found.");
            }
        }

        public async Task<SalonService> UpdateSalonService(int serviceId, SalonService salonService)
        {
            if (serviceId == salonService.ServiceId)
            {
                _context.SalonService.Update(salonService);
                await _context.SaveChangesAsync();
                return salonService;
            }
            else
            {
                throw new KeyNotFoundException("Problem with updating...");
            }
        }

    }
}

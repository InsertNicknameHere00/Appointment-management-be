using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository
{
    public class SalonServiceRepository : ISalonServiceRepository
    {
        private readonly HaircutSalonDbContext _context;
        public SalonServiceRepository(HaircutSalonDbContext context)
        {
            _context = context;

        }

        public async Task<SalonService> AddSalonService(SalonService salonService)
        {
            await _context.SalonService.AddAsync(salonService);
            await _context.SaveChangesAsync();
            return salonService;
        }

        public async Task<bool> DeleteSalonService(SalonService service)
        {
            _context.SalonService.Remove(service);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<SalonService>> GetAllSalonServices()
        {
            return await _context.SalonService.ToListAsync();
        }

        public async Task<SalonService> GetSalonServicesById(int serviceId)
        {
            return await _context.SalonService.FindAsync(serviceId);

        }

        public async Task<SalonService> UpdateSalonService(int serviceId, SalonService salonService)
        {
            _context.SalonService.Update(salonService);
            await _context.SaveChangesAsync();
            return salonService;
        }

        public SalonService Search(int id)
        {
            var service = _context.SalonService.Find(id);
            return service;

        }

    }
}

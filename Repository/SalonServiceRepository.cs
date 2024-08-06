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
            /*var currentService = new SalonService();
            currentService.ServiceId = salonService.ServiceId;
            currentService.ServiceTitle = salonService.ServiceTitle;
            currentService.ServiceDescription = salonService.ServiceDescription;
            await _context.SalonService.AddAsync(currentService);
            await _context.SaveChangesAsync();
            return currentService;*/
            await _context.SalonService.AddAsync(salonService);
            await _context.SaveChangesAsync();
            return salonService;
        }

        public async Task<bool> DeleteSalonService(SalonService service)
        {
            //var service = await _context.SalonService.FindAsync(id);
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
            /*  var currentService = await _context.SalonService.FindAsync(serviceId);
              if (currentService != null)
              {
                  currentService.ServiceId = serviceId;
                  currentService.ServiceTitle = salonService.ServiceTitle;
                  currentService.ServiceDescription = salonService.ServiceDescription;
                  _context.SalonService.Update(currentService);
                  await _context.SaveChangesAsync();
                  return currentService;
              }
              else
              {
                  throw new KeyNotFoundException();
              }*/
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

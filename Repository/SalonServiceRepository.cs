using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository
{
    public class SalonServiceRepository : HaircutSalonDbContext,ISalonServiceRepository
    {
        public SalonServiceRepository(DbContextOptions<HaircutSalonDbContext> options) : base(options)
        {
        }

        public async Task AddSalonService(SalonService salonService)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSalonService(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SalonService>> GetAllSalonServices()
        {
            return await SalonService.ToListAsync();
        }

        public Task GetSalonServicesById(int serviceId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSalonService(int serviceId, SalonService salonService)
        {
            throw new NotImplementedException();
        }
    }
}

using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;

namespace AppointmentAPI.Services
{
    public class SalonServices : ISalonServices
    {
        private readonly ISalonServiceRepository _salonServiceRepository;
        public SalonServices(ISalonServiceRepository salonServiceRepository)
        {
            _salonServiceRepository = salonServiceRepository;
        }


        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SalonService>> GetAll()
        {
            return await _salonServiceRepository.GetAllSalonServices();
        }

        public SalonService GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(SalonService salonService)
        {
            throw new NotImplementedException();
        }

        public bool Update(SalonService salonService)
        {
            throw new NotImplementedException();
        }
    }
}

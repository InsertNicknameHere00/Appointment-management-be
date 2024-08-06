using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using System.Runtime.InteropServices;

namespace AppointmentAPI.Services
{
    public class SalonServices:ISalonServices
    {
        private readonly ISalonServiceRepository _repository;
        public SalonServices(ISalonServiceRepository salonServiceRepository)
        {
            _repository = salonServiceRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var result=_repository.Search(id);
            if (result != null)
            {
                await _repository.DeleteSalonService(result);
                return true;
            }
            else
            {
                return false;
            }
            //return await _repository.DeleteSalonService(id);

        }

        public async Task<List<SalonService>> GetAllSalonServices()
        {
            return await _repository.GetAllSalonServices();
        }

        public async Task<SalonService> GetSalonServiceById(int id)
        {
            return await _repository.GetSalonServicesById(id);
        }

        public async Task<SalonService> Save(SalonService salonService)
        {
            return await _repository.AddSalonService(salonService);
            
        }

        public async Task<SalonService> Update(int serviceId,SalonService salonService)
        {
            /*var result = _repository.Search(serviceId);
            if (result != null)
            {
                await _repository.UpdateSalonService(serviceId,result);
                return result;
            }
            else
            {
                throw new Exception();
            }*/
            return await _repository.UpdateSalonService(serviceId,salonService);
        }
    }
}

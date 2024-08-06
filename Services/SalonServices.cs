using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using Azure.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AppointmentAPI.Services
{
    public class SalonServices : ISalonServices
    {
        private readonly ISalonServiceRepository _repository;
        public SalonServices(ISalonServiceRepository salonServiceRepository)
        {
            _repository = salonServiceRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var result = _repository.Search(id);
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
            var result = await _repository.GetAllSalonServices();
            if (result.Count != 0)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.GetAllSalonServices();
        }

        public async Task<SalonService> GetSalonServiceById(int id)
        {
            //return await _repository.GetSalonServicesById(id);
            var result = await _repository.GetSalonServicesById(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<SalonService> Save(SalonService salonService)
        {

            if (salonService != null)
            {
                var result = new SalonService();
                result.ServiceId = salonService.ServiceId;
                result.ServiceTitle = salonService.ServiceTitle;
                result.ServiceDescription = salonService.ServiceDescription;
                await _repository.AddSalonService(result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
            //return await _repository.AddSalonService(salonService);

        }

        public async Task<SalonService> Update(int serviceId, SalonService salonService)
        {
            var result = await _repository.GetSalonServicesById(serviceId);
            if (result != null)
            {
                result.ServiceId = serviceId;
                result.ServiceTitle = salonService.ServiceTitle;
                result.ServiceDescription = salonService.ServiceDescription;
                await _repository.UpdateSalonService(serviceId, result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
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
            //return await _repository.UpdateSalonService(serviceId, salonService);
        }
    }
}

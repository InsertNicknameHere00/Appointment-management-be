using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class SalonServiceController : ControllerBase
    {
        private readonly ISalonServices salonService;

        public SalonServiceController(ISalonServices _service)
        {
            salonService = _service;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalonService>>> GetAll()
        {
            // return await salonService.GetAll();
            var services = await salonService.GetAllSalonServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalonService>> GetServiceById(int id)
        {
            return await salonService.GetSalonServiceById(id);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalonService(int id, SalonService _service)
        {
            await salonService.Update(id,_service);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<SalonService>> AddSalonService(SalonService _service)
        {
            await salonService.Save(_service);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalonService(int id)
        {
            await salonService.Delete(id);
            return Ok();
        }

       
    }
}

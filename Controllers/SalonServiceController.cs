using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var services = await salonService.GetAllSalonServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalonService>> GetServiceById([FromHeader] int id)
        {
            var service= await salonService.GetSalonServiceById(id);
            return Ok(service);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalonService([FromHeader] int id,[FromBody] SalonService _service)
        {
            //try-catch
            var service=await salonService.Update(id,_service);
            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<SalonService>> AddSalonService([FromBody] SalonService _service)
        {
            var service= await salonService.Save(_service);
            return Ok(service);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalonService([FromHeader] int id)
        {
            var service=await salonService.Delete(id);
            return Ok(service);
        }

       
    }
}

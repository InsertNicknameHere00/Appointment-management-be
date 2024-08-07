using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class AdminServiceController : ControllerBase
    {
        private readonly IAdminServices adminService;

        public AdminServiceController(IAdminServices _service)
        {
            adminService = _service;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminService>>> GetAll()
        {
            var services = await adminService.GetAllAdminServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminService>> GetServiceById([FromHeader] int id)
        {
            var service = await adminService.GetAdminServicesById(id);
            return Ok(service);
            
        }

        //[Authorize(Roles ="Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdminService([FromHeader] int id, [FromBody] AdminService _service)
        {
            var service = await adminService.Update(id,_service);
            return Ok(service);
        }


        [HttpPost]
        public async Task<ActionResult<AdminService>> AddAdminService([FromBody] AdminService _service)
        {
            var service = await adminService.Save(_service);
            return Ok(service);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminService([FromHeader] int id)
        {
            var service = await adminService.Delete(id);
            return Ok(service);
        }

       
    }
}

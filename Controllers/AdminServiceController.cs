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
        public async Task<ActionResult<AdminService>> GetServiceById(int id)
        {
            return await adminService.GetAdminServicesById(id);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdminService(int id, AdminService _service)
        {
            await adminService.Update(id,_service);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<AdminService>> AddAdminService(AdminService _service)
        {
            await adminService.Save(_service);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminService(int id)
        {
            await adminService.Delete(id);
            return Ok();
        }

       
    }
}

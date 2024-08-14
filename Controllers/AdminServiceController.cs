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
    public class AdminServiceController : ControllerBase
    {
        private readonly IAdminServices adminService;
        private readonly ILogger<AdminServiceController> logger;

        public AdminServiceController(IAdminServices _service, ILogger<AdminServiceController> logger)
        {
            adminService = _service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminService>>> GetAll()
        {
            try
            {
                var services = await adminService.GetAllAdminServices();
                logger.LogInformation("Successful request");
                return Ok(services);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminService>> GetServiceById([FromHeader] int id)
        {
            try
            {
                var service = await adminService.GetAdminServicesById(id);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdminService([FromHeader] int id, [FromBody] AdminService _service)
        {
            try
            {
                var service = await adminService.Update(id, _service);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdminService>> AddAdminService([FromBody] AdminService _service)
        {
            try
            {
                var service = await adminService.Save(_service);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdminService([FromHeader] int id)
        {
            try
            {
                var service = await adminService.Delete(id);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<AdminService>>> GetAllAdminServicesByAdminId([FromHeader] int adminId)
        {
            try
            {
                var services = await adminService.GetAdminServiceByAdminId(adminId);
                logger.LogInformation("Successful request");
                return Ok(services);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("service/{id}")]
        public async Task<ActionResult<IEnumerable<AdminService>>> GetAllAdminServicesByServiceId([FromHeader] int serviceId)
        {
            try
            {
                var services = await adminService.GetAdminServiceByServiceId(serviceId);
                logger.LogInformation("Successful request");
                return Ok(services);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }


    }
}

using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonServiceController : ControllerBase
    {
        private readonly ISalonServices salonService;
        private readonly ILogger<SalonServiceController> logger;

        public SalonServiceController(ISalonServices _service, ILogger<SalonServiceController> _logger)
        {
            salonService = _service;
            logger = _logger;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalonService>>> GetAll()
        {
            try
            {
                var services = await salonService.GetAllSalonServices();
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
        public async Task<ActionResult<SalonService>> GetServiceById([FromHeader] int id)
        {
            try
            {
                var services = await salonService.GetSalonServiceById(id);
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSalonService([FromHeader] int id, [FromBody] SalonService _service)
        {
            try
            {
                var services = await salonService.Update(id, _service);
                logger.LogInformation("Successful request");
                return Ok(services);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SalonService>> AddSalonService([FromBody] SalonService _service)
        {
            try
            {
                var services = await salonService.Save(_service);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSalonService([FromHeader] int id)
        {
            try
            {
                var services = await salonService.Delete(id);
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

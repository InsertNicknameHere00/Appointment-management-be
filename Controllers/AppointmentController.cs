namespace AppointmentAPI.Controllers
{
    using AppointmentAPI.Entities;
    using AppointmentAPI.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        private readonly ILogger<AdminServiceController> logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AdminServiceController> logger)
        {
            this.appointmentService = appointmentService;
            this.logger = logger;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var appointmentsTemp = await this.appointmentService.GetAll();
                logger.LogInformation("Successful request");

                return Ok(appointmentsTemp);
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

        [HttpGet("id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromHeader] int id)
        {
            try
            {
                var appointmentExists = await this.appointmentService.ExistsByIdAsync(id);
                if (appointmentExists == false)
                {
                    throw new Exception();
                }
                else
                {
                    var appointmentTemp = await this.appointmentService.GetById(id);
                    logger.LogInformation("Successful request");

                    return Ok(appointmentTemp);
                }
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

        [HttpPost("add/id")]
        public async Task<IActionResult> Add([FromHeader] int userId, int serviceId)
        {
            try 
            {
                var appointmentTemp = await this.appointmentService.CreateAsync(userId, serviceId);
                logger.LogInformation("Successful request");

                return Ok(appointmentTemp);
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

        [HttpPut("edit/id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([FromHeader] int id, int userId, string status)
        {
            try
            {
                var appointmentTemp = await this.appointmentService.EditAsync(id, userId, status);
                logger.LogInformation("Successful request");

                return Ok(appointmentTemp);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status304NotModified, new { message = "Error while editing an appointment" });
            }
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromHeader] int id)
        {
            try
            {
                await this.appointmentService.DeleteAsync(id);
                logger.LogInformation("Successful request");

                return Ok();
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

        [HttpPut("book/id")]
        [AllowAnonymous]
        public async Task<IActionResult> Book([FromHeader] int id, int clientId)
        {
            try
            {
                await this.appointmentService.BookAnAppointment(id, clientId);
                logger.LogInformation("Successful request");

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status304NotModified, new { message = "Error while booking an appointment" });
            }
        }

        [HttpPut("cancel/id")]
        [AllowAnonymous]
        public async Task<IActionResult> Cancel([FromHeader] int id, int clientId)
        {
            try
            {
                await this.appointmentService.CancelAnAppointment(id, clientId);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status304NotModified, new { message = "Error while canceling an appointment"});
            }
        }
    }
}

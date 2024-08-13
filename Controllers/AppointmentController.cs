namespace AppointmentAPI.Controllers
{
    using AppointmentAPI.Entities;
    using AppointmentAPI.Services;
    using AppointmentAPI.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var appointmentsTemp = await this.appointmentService.GetAll();

                return Ok(appointmentsTemp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
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

                    return Ok(appointmentTemp);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Appointment not found");
            }
        }

        [HttpPost("add/id")]
        public async Task<IActionResult> Add([FromHeader] int userId, int serviceId)
        {
            try 
            {
                var appointmentTemp = await this.appointmentService.CreateAsync(userId, serviceId);

                return Ok(appointmentTemp);
            } 
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPut("edit/id")]
        public async Task<IActionResult> Edit([FromHeader] int id, int userId, string status)
        {
            try
            {
                var appointmentTemp = await this.appointmentService.EditAsync(id, userId, status);

                return Ok(appointmentTemp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status304NotModified,
                 "Error editing data for appointment");
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete([FromHeader] int id)
        {
            try
            {
                await this.appointmentService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from the database");
            }
        }

        [HttpPut("book/id")]
        [AllowAnonymous]
        public async Task<IActionResult> Book([FromHeader] int id, int clientId)
        {
            try
            {
                await this.appointmentService.BookAnAppointment(id, clientId);

                return Ok();
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status304NotModified, new { message = "Error while canceling an appointment"});
            }
        }
    }
}

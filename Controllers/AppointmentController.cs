namespace AppointmentAPI.Controllers
{
    using AppointmentAPI.Entities;
    using AppointmentAPI.Services;
    using AppointmentAPI.Services.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var appointmentsTemp = await this.appointmentService.GetAll();

            return Ok(appointmentsTemp);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromHeader] int id)
        {
            var appointmentTemp = await this.appointmentService.GetById(id);
            if (appointmentTemp == null)
            { 
                return NotFound();
            }

            return Ok(appointmentTemp);
        }

        [HttpPost("id")]
        public async Task<IActionResult> Add([FromHeader] int userId)
        {
            var appointmentTemp = await this.appointmentService.CreateAsync(userId);

            return Ok(appointmentTemp);
        }

        [HttpPut("edit/id")]
        public async Task<IActionResult> Edit([FromHeader] int id, int userId, string status)
        {
            var appointmentTemp = await this.appointmentService.EditAsync(id, userId, status);

            return Ok(appointmentTemp);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete([FromHeader] int id)
        {
            await this.appointmentService.DeleteAsync(id);

            return Ok();
        }
    }
}

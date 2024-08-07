using AppointmentAPI.Entities;
using AppointmentAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailNotificationsController : ControllerBase
    {
        private readonly IEmailSendService _mailService;
        private readonly ILogger<EmailNotificationsController> _logger;
        public EmailNotificationsController(IEmailSendService mailService, ILogger<EmailNotificationsController> logger) 
        {
            this._mailService = mailService;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] EmailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                _logger.LogInformation("Successful request");
                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

    }
}

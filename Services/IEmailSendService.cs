using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IEmailSendService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
    }
}

using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IEmailSendService
    {
        Task SendEmail(EmailRequest emailRequest);
    }
}

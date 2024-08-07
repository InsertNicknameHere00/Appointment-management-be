using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IEmailSendService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
    }
}

using AppointmentAPI.Entities;

using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AppointmentAPI.Services
{
    public class EmailSendService : IEmailSendService
    {
        private readonly EmailSettings _mailSettings;

        public EmailSendService(IOptions<EmailSettings> mailSettings) 
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmail(EmailRequest mailRequest)
        {
            _mailSettings.EmailAddress = "appointmentapi0@gmail.com";
            _mailSettings.SenderName = "AppointmentAPI";
            _mailSettings.Password = "cmwf tleg fiqv azjp";
            _mailSettings.Host = "smtp.gmail.com";
            _mailSettings.Port = 587;

            var fromAddress = new MailAddress(_mailSettings.EmailAddress, _mailSettings.SenderName);
            var toAddress = new MailAddress(mailRequest.ToEmail, "To Name");

            var smtp = new SmtpClient();
            smtp.Host = _mailSettings.Host;
            smtp.Port = _mailSettings.Port;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials=new NetworkCredential(fromAddress.Address,_mailSettings.Password);

            using (var email = new MailMessage(fromAddress, toAddress)
            {
                Subject=mailRequest.Subject,
                Body=mailRequest.Body
            }) 
                smtp.Send(email);
        }
    }
}

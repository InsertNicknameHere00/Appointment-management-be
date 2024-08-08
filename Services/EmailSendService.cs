using AppointmentAPI.Entities;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace AppointmentAPI.Services
{
    public class EmailSendService : IEmailSendService
    {
        private readonly EmailSettings _mailSettings;
        public EmailSendService(IOptions<EmailSettings> mailSettings) 
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(EmailRequest mailRequest)
        {

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.EmailAddress);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject=mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.EmailAddress, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
           // throw new NotImplementedException();
        }
    }
}

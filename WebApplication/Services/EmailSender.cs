using System.Net;
using System.Net.Mail;

namespace WebApplication.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(string recipient, string subject, string message)
        {
            var smtpServer = _config["Smtp:Server"];
            var smtpPort = int.Parse(_config["Smtp:Port"]);
            var sender = _config["Smtp:Sender"];
            var password = _config["Smtp:Password"];

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(sender, password),
                EnableSsl = true
            };

            using var mail = new MailMessage
            {
                From = new MailAddress(sender),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mail.To.Add(recipient); 

            await client.SendMailAsync(mail);
        }
    }
}

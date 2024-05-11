using System.Net.Mail;
using System.Net;
using WebApp.Settings;
using Microsoft.Extensions.Options;

namespace WebApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpSetting> smtpSetting;

        public EmailService(IOptions<SmtpSetting> smtpSetting)
        {
            this.smtpSetting = smtpSetting;
        }
        public async Task Send(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from, to, subject, body);

            using (var emailClient = new SmtpClient(smtpSetting.Value.Host, smtpSetting.Value.Port)) // adres hosta, port
            {
                emailClient.Credentials = new NetworkCredential(smtpSetting.Value.User, smtpSetting.Value.Password); // username, hasło
                await emailClient.SendMailAsync(message);
            }
        }
    }
}

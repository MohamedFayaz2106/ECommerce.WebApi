using ECommerce.BusinessModel;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECommerce.WebApi.Services
{
    public class Mailer : IMailer
    {
        private readonly ILogger<Mailer> Logger;
        private IConfiguration Configuration { get; }
        private SmtpSettings SmtpSettings { get; set; }

        public Mailer(IConfiguration configuration, ILogger<Mailer> logger)
        {
            Configuration = configuration;
            Logger = logger;
            SmtpSettings = new SmtpSettings();
            Configuration.GetSection("SmtpSettings").Bind(SmtpSettings);
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string body)
        {
            try
            { 
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(SmtpSettings.SenderName, SmtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Plain) { Text = body };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(SmtpSettings.Server, SmtpSettings.Port,true);
                    await client.AuthenticateAsync(SmtpSettings.Username, SmtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message);
                return false;
            }

            return true;

      }
    }
}

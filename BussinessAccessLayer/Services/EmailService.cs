using Management.Common.Models.Entity;
using Management.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Management.Common.Models;

namespace Management.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;

        public EmailService(EmailConfiguration emailConfiguration,IConfiguration configuration)
        {
            _emailConfig = emailConfiguration;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var from = _configuration.GetSection("EmailConfiguration:From").Value;
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", from));
            var parsedRecipients = MimeKit.MailboxAddress.Parse(message.To);
            emailMessage.To.Add((InternetAddress)parsedRecipients);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
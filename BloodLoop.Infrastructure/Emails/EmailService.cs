using BloodCore.Emails;
using System;
using System.Threading.Tasks;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using BloodLoop.Infrastructure.IoC;
using BloodCore;

namespace BloodLoop.Infrastructure.Emails
{
    [Injectable]
    internal class SmtpEmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly SmtpEmailSettings _smtpSettings;

        public SmtpEmailService(EmailSettings emailSettings, SmtpEmailSettings smtpSettings)
        {
            _emailSettings = emailSettings;
            _smtpSettings = smtpSettings;
        }

        public async Task<bool> SendEmail<TEmailTemplate>(TEmailTemplate template, string email) where TEmailTemplate : EmailTemplate
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = template.Subject;

            message.Body = new TextPart("html")
            {
                Text = template.Print()
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Url, _smtpSettings.Port, false);

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Password);

                await client.SendAsync(message);
                client.Disconnect(true);
            }

            return true;
        }
    }
}

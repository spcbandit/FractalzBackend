using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Fractalz.Infrastructure.EmailService.Adaptors
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _options;
        
        /// <summary>
        /// Конструктор EmailService
        /// </summary>
        /// <param name="options"></param>
        /// <exception cref="ArgumentException"></exception>
        public EmailService(IOptions<EmailServiceOptions> options)
        {
            _options = options != null ? options.Value : throw new ArgumentException(nameof(options));
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public async Task SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress(_options.Name, _options.FromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = CreateLatter(subject, message)
                
            };
             
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.HostAddress, _options.Port, false);
                await client.AuthenticateAsync(_options.Name, _options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        
        /// <summary>
        /// CreateLatter
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private string CreateLatter(string subject, string content)
        {
            string letter = String.Format(
                $"<p>{subject}</p><p>Ваш код : {content}</p>");
            return letter;
        }
    }
}
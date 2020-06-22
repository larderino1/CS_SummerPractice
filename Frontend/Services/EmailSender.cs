using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services
{
    // this service helps working with email service SendGrid
    public class EmailSender : IEmailSender
    {
        private IConfiguration _config;
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(subject, htmlMessage, email);
        }

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public Task Execute(string subject, string message, string email)
        {
            var apiKey = _config.GetValue<string>("SendGridOnlineShopKey");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("i.ishchenko@ukma.edu.ua", "Online Shop Team"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(true, true);

            return client.SendEmailAsync(msg);
        }
    }
}

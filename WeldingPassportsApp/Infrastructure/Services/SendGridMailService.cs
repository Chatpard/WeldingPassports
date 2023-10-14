using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SendGridMailService : IMailService
    {
        private readonly IConfiguration _config;

        public SendGridMailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _config["SendGridAPIKey"];
            ArgumentException.ThrowIfNullOrEmpty(apiKey, nameof(apiKey));
            var client = new SendGridClient(apiKey);
            ArgumentException.ThrowIfNullOrEmpty(_config["SendGridMailFrom"]);
            ArgumentException.ThrowIfNullOrEmpty(_config["SendGridNameFrom"]);
            //Note: Do not use key "SendGridEmailFrom". It reserved to return the SendGrid account Email.
            var from = new EmailAddress(_config["SendGridMailFrom"], _config["SendGridNameFrom"]);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}

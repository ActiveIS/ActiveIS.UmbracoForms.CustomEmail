using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Umbraco.Core.Logging;

namespace ActiveIS.UmbracoForms.CustomEmail.Helpers
{
    internal class HandleSmtp
    {
        private readonly ILogger _logger;

        private HandleSmtp(ILogger logger)
        {
            _logger = logger;
        }
        internal void SendEmail(string emailBody, string toEmail, string fromEmail, string fromName, string subject)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true,
            };

            var toEmailAddresses = toEmail.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (toEmailAddresses.Length == 0)
            {
                throw new Exception("No to addresses have been defined");
            }


            if (toEmailAddresses.Length > 1)
            {
                foreach (var toEmailAddress in toEmailAddresses)
                {
                    message.To.Add(toEmailAddress);
                }
            }

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
            }
        }
    }
}

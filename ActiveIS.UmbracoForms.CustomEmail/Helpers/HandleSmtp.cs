using System;
using System.Net.Mail;
using System.Net.Mime;
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
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true,
            };

            var toEmailAddresses = toEmail.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (toEmailAddresses.Length == 0)
            {
                throw new Exception("No to addresses have been defined");
            }

            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrWhiteSpace(fromEmail))
            {
                throw new Exception("No from address has been defined");
            }

            message.From = !string.IsNullOrWhiteSpace(fromName) ? new MailAddress(fromEmail, fromName) : new MailAddress(fromEmail);

            if (toEmailAddresses.Length > 1)
            {
                foreach (var toEmailAddress in toEmailAddresses)
                {
                    message.To.Add(toEmailAddress);
                }
            }

            //Add plain text body version
            var mimeType = new ContentType("text/html");
            var plainText = AlternateView.CreateAlternateViewFromString(emailBody, mimeType);
            message.AlternateViews.Add(plainText);
            try
            {
                SmtpClient client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(HandleSmtp), ex);
            }
        }
    }
}

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
            MailMessage message = new MailMessage();

            var toEmailAddresses = toEmail.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (toEmailAddresses.Length == 0)
            {
                throw new Exception("No to addresses have been defined");
            }

            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrWhiteSpace(fromEmail))
            {
                throw new Exception("No from address has been defined");
            }

            if (string.IsNullOrEmpty(subject) || string.IsNullOrWhiteSpace(subject))
            {
                throw new Exception("No subject has been defined");
            }

            if (string.IsNullOrEmpty(emailBody) || string.IsNullOrWhiteSpace(emailBody))
            {
                throw new Exception("No email body has been defined");
            }

            if (toEmailAddresses.Length > 1)
            {
                foreach (var toEmailAddress in toEmailAddresses)
                {
                    message.To.Add(toEmailAddress);
                }
            }

            message.Subject = subject;

            message.From = !string.IsNullOrWhiteSpace(fromName) ? new MailAddress(fromEmail, fromName) : new MailAddress(fromEmail);

            //Add plain text body version
            var mimeType = new ContentType("text/html");
            var plainText = AlternateView.CreateAlternateViewFromString(emailBody, mimeType);
            message.AlternateViews.Add(plainText);

            message.IsBodyHtml = true;
            message.Body = emailBody;

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

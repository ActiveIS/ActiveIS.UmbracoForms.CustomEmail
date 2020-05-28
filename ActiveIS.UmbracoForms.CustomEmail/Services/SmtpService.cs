using System;
using System.Net.Mail;
using System.Net.Mime;
using ActiveIS.UmbracoForms.CustomEmail.Interfaces;
using Umbraco.Core.Logging;

namespace ActiveIS.UmbracoForms.CustomEmail.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly ILogger _logger;

        public SmtpService()
        {
        }

        public SmtpService(ILogger logger)
        {
            _logger = logger;
        }

        public void SendEmail(string emailBody, string toEmail, string fromEmail, string fromName, string subject)
        {
            var message = new MailMessage();

            //Create array of to addresses
            var toEmailAddresses = toEmail.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);

            //Throw exceptions if the basics are missing
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

            //Add to addresses
            if (toEmailAddresses.Length > 1)
            {
                foreach (var toEmailAddress in toEmailAddresses)
                {
                    message.To.Add(toEmailAddress);
                }
            }

            //Add subject
            message.Subject = subject;

            //Add from email or from email with name
            message.From = !string.IsNullOrWhiteSpace(fromName) ? new MailAddress(fromEmail, fromName) : new MailAddress(fromEmail);

            //Add plain text body version
            var mimeType = new ContentType("text/html");
            var plainText = AlternateView.CreateAlternateViewFromString(emailBody, mimeType);
            message.AlternateViews.Add(plainText);

            //Add html body version
            message.IsBodyHtml = true;
            message.Body = emailBody;

            //Try to send the email
            try
            {
                var client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(HandleSmtp), ex);
            }
        }
    }
}

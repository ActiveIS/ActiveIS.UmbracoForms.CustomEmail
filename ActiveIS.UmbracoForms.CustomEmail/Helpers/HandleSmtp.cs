using System;
using System.Net.Mail;

namespace ActiveIS.UmbracoForms.CustomEmail.Helpers
{
    internal static class HandleSmtp
    {
        internal static void SendEmail(string emailBody, string toEmail, string fromEmail, string fromName, string subject)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true,
            };

            message.To.Add(toEmail);


            SmtpClient client = new SmtpClient();

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

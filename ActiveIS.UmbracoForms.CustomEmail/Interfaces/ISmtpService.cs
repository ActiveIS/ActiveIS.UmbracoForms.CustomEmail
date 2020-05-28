namespace ActiveIS.UmbracoForms.CustomEmail.Interfaces
{
    public interface ISmtpService
    {
        void SendEmail(string emailBody, string toEmail, string fromEmail, string fromName, string subject);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using ActiveIS.UmbracoForms.CustomEmail.Helpers;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;

namespace ActiveIS.UmbracoForms.CustomEmail.Workflows
{
    public class CustomEmailWorkflow : WorkflowType
    {
        public CustomEmailWorkflow()
        {
            Id = new Guid("1e106db8-685d-441f-9c19-c5e344163c2c");
            Name = "Send Custom Email";
            Description = "This workflow is is to be used to send a custom templated email";
            Icon = "icon-message";
            Group = "Services";
        }

        [Setting("To Email", Description = "Enter the receiver email", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string ToEmail { get; set; }

        [Setting("From Email", Description = "Enter the sender email", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string FromEmail { get; set; }

        [Setting("Company Name", Description = "Enter a company name to head the email and show as the from name", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string CompanyName { get; set; }

        [Setting("Email Heading", Description = "Enter a heading for the email", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string Heading { get; set; }

        [Setting("Subject", Description = "Email subject", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string Subject { get; set; }

        [Setting("Message", Description = "Enter the intro message", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textarea.html")]
        public string Message { get; set; }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            var path = "~/Views/Partials/Forms/BasicEmails/Basic.html";
            var emailBody = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(path))
                .Replace("[[COMPANY NAME]]", CompanyName)
                .Replace("[[HEADING]]", Heading)
                .Replace("[[BODY]]", Message.Replace("\n", "<br />")
                    .Replace("\r", "<br />"));


            HandleSmtp.SendEmail(emailBody, ToEmail, FromEmail, CompanyName, Subject);

            //record.State = FormState.Approved;
            return WorkflowExecutionStatus.Completed;
        }

        public override List<Exception> ValidateSettings()
        {
            var exceptions = new List<Exception>();
            if (string.IsNullOrEmpty(Message) || string.IsNullOrWhiteSpace(Message))
            {
                exceptions.Add(new Exception("Message is required"));
            }

            if (string.IsNullOrEmpty(ToEmail) || string.IsNullOrWhiteSpace(ToEmail))
            {
                exceptions.Add(new Exception("To email is required"));
            }

            return exceptions;
        }
    }
}

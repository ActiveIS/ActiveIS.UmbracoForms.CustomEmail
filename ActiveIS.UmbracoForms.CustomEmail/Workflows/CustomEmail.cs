using System;
using System.Collections.Generic;
using System.IO;
using ActiveIS.UmbracoForms.CustomEmail.Interfaces;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;

namespace ActiveIS.UmbracoForms.CustomEmail.Workflows
{
    public class CustomEmailWorkflow : WorkflowType
    {
        private readonly ISmtpService _smtpService;
        private readonly ILogger _logger;
        public CustomEmailWorkflow(ISmtpService smtpService, ILogger logger)
        {
            _smtpService = smtpService;
            _logger = logger;
            Id = new Guid("1e106db8-685d-441f-9c19-c5e344163c2c");
            Name = "Send Custom Email";
            Description = "This workflow is is to be used to send a custom templated email";
            Icon = "icon-message";
            Group = "Services";
        }

        [Setting("To Email", Description = "Enter the receiver email (Seperate multiple with a comma or semi-colon)", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string ToEmail { get; set; }

        [Setting("From Email", Description = "Enter the sender email", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string FromEmail { get; set; }

        [Setting("From Name", Description = "Enter the sender name", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string FromName { get; set; }

        [Setting("Email Heading", Description = "Enter a heading for the email", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string Heading { get; set; }

        [Setting("Subject", Description = "Email subject", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string Subject { get; set; }

        [Setting("Message", Description = "Enter the intro message", View = "~/App_Plugins/Mw.UmbForms.Rte/editor.html")]
        public string Message { get; set; }

        [Setting("Template Name", Description = "Enter the template name (defaults to \"CustomTemplate\")", View = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/textfield.html")]
        public string TemplateName { get; set; }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            try
            {
                var template = "~/Views/Partials/Forms/CustomEmails/CustomTemplate.html";
                if (!string.IsNullOrEmpty(TemplateName) || !string.IsNullOrWhiteSpace(TemplateName))
                {
                    template = $"~/Views/Partials/Forms/CustomEmails/{TemplateName}.html";
                }

                var emailBody = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(template))
                    .Replace("[[SUBJECT]]", Subject)
                    .Replace("[[HEADING]]", Heading)
                    .Replace("[[BODY]]", Message);

                _smtpService.SendEmail(emailBody, ToEmail, FromEmail, FromName, Subject);
                return WorkflowExecutionStatus.Completed;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(CustomEmailWorkflow), ex);
                return WorkflowExecutionStatus.Failed;
            }
        }

        public override List<Exception> ValidateSettings()
        {
            var exceptions = new List<Exception>();
            if (string.IsNullOrEmpty(Message) || string.IsNullOrWhiteSpace(Message))
            {
                exceptions.Add(new Exception("Message is required"));
            }

            if (string.IsNullOrEmpty(Subject) || string.IsNullOrWhiteSpace(Subject))
            {
                exceptions.Add(new Exception("Subject is required"));
            }

            if (string.IsNullOrEmpty(ToEmail) || string.IsNullOrWhiteSpace(ToEmail))
            {
                exceptions.Add(new Exception("To email is required"));
            }

            if (string.IsNullOrEmpty(FromEmail) || string.IsNullOrWhiteSpace(FromEmail))
            {
                exceptions.Add(new Exception("From email is required"));
            }

            return exceptions;
        }
    }
}

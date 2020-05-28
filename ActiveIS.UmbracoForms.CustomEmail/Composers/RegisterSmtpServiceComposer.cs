using ActiveIS.UmbracoForms.CustomEmail.Interfaces;
using ActiveIS.UmbracoForms.CustomEmail.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace ActiveIS.UmbracoForms.CustomEmail.Composers
{
    public class RegisterSmtpServiceComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ISmtpService, SmtpService>(Lifetime.Request);
        }
    }
}

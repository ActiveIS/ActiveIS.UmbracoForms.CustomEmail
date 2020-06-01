using System.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace ActiveIS.UmbracoForms.CustomEmail.Controllers
{
    [PluginController("ActiveISCustomEmail")]
    public class PickerController : UmbracoAuthorizedJsonController
    {
        public object GetVirtualPathForEmailTemplate(string encodedPath)
        {
            return new
            {
                path = ("Forms/CustomEmails/" + HttpUtility.UrlDecode(encodedPath))
            };
        }
    }
}

using Umbraco.Core.IO;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace ActiveIS.UmbracoForms.CustomEmail.Trees
{
    [Tree("forms", "activeisCustomEmailTemplates", IsSingleNodeTree = true, TreeTitle = "Custom Email Templates", TreeUse = TreeUse.Dialog)]
    [PluginController("ActiveISCustomEmail")]
    public class ActiveISCustomEmailTemplateTreeController : FileSystemTreeController
    {
        private static readonly string[] ExtensionsStatic = new string[1]
        {
            "html"
        };

        protected override IFileSystem FileSystem
        {
            get
            {
                return (IFileSystem)new PhysicalFileSystem("~/Views/Partials/Forms/CustomEmails");
            }
        }

        protected override string[] Extensions
        {
            get
            {
                return ActiveISCustomEmailTemplateTreeController.ExtensionsStatic;
            }
        }

        protected override string FileIcon
        {
            get
            {
                return "icon-article";
            }
        }
    }
}

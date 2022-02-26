using MultipleTenancyAzureAD.Core;
using MultiTenancyAzureAD.Core;
using System.Web.Mvc;

namespace MultiTenancyAzureAD.Main.Extensions
{
    public class MultiTenantViewEngine : RazorViewEngine
    {
        public MultiTenantViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/%1/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/%1/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
            MasterLocationFormats = new[]
            {
                "~/Views/%1/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/%1/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/%1/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/%1/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            if (TenantHelper.Tenant != null)
            {
                return base.CreatePartialView(controllerContext, partialPath.Replace("%1", TenantHelper.Tenant.Name));
            }
            else
            {
                return base.CreatePartialView(controllerContext, partialPath);
            }
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            if (TenantHelper.Tenant != null)
            {
                return base.CreateView(controllerContext,
               viewPath.Replace("%1", TenantHelper.Tenant.Name),
               masterPath.Replace("%1", TenantHelper.Tenant.Name));
            }
            else
            {
                return base.CreateView(controllerContext, viewPath, masterPath);
            }
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            if(TenantHelper.Tenant != null)
            {
                return base.FileExists(controllerContext, virtualPath.Replace("%1", TenantHelper.Tenant.Name));
            }
            else
            {
                return base.FileExists(controllerContext, virtualPath);
            }
        }
    }
}
using MultipleTenancyAzureAD.Core;
using MultiTenancyAzureAD.Core;
using System.Web.Mvc;

namespace MultiTenancyAzureAD.Main.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        { }

        public ActionResult Index()
        {
            Tenant t = TenantHelper.Tenant;
            ViewBag.TenantName = t != null ? t.Name : "UNKNOWN";
            ViewBag.TenantId = t != null ? t.Id : "UNKNOWN";
            // fix issue when loading dynamic template in the azure environment:
            // dont know why not work, have to specific the template here
            if(ViewBag.TenantName != "UNKNOWN")
            {
                return View($"~/Views/{t.Name}/Home/Index.cshtml");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
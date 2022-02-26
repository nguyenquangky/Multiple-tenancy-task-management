using MultiTenancyAzureAD.Core;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MultipleTenancyAzureAD.Core
{
    public static class TenantHelper
    {
        //private static ClaimsIdentity UserClaims
        //{
        //    get
        //    {
        //        return HttpContext.Current.User.Identity as ClaimsIdentity;
        //    }
        //}
        public static Tenant Tenant
        {
            get
            {
                if (HttpContext.Current.User != null)
                {
                    var claim = HttpContext.Current.User.Identity as ClaimsIdentity;
                    Tenant t = new Tenant();
                    if (claim.IsAuthenticated)
                    {
                        t.Id = claim?.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value;

                        if (t.Id == null)
                        {
                            throw new InvalidOperationException("Tenant is not found");
                        }
                        t.Name = ConfigurationManager.AppSettings[$"tenantName:{t.Id}"]?.ToString();
                        t.ConnectionString = ConfigurationManager.AppSettings[$"tenantConnectionString:{t.Id}"]?.ToString();
                        return t;
                    }
                    else
                    {
                        // not autheticated user
                        return null;
                    }
                }
                else
                {
                    // anomynous user
                    return null;
                }
            }
        }

        public static string UserName
        {
            get
            {
                if(HttpContext.Current.User != null)
                {
                    var claim = HttpContext.Current.User.Identity as ClaimsIdentity;
                    return claim.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}

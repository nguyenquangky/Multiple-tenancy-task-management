using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MultiTenancyAzureAD.Main
{
    public partial class Startup
    {
        private string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private string Authority = "https://login.microsoftonline.com/organizations/v2.0";
        public void ConfigureAuth(IAppBuilder app)
        {
            List<string> tenantIds = new List<string>();
            foreach(string item in ConfigurationManager.AppSettings["tenant:Id"]?.Split(';').ToList())
            {
                tenantIds.Add($"https://login.microsoftonline.com/{item}/v2.0");
            }

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions { });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = ClientId,
                    Authority = Authority,
                    TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        // instead of using the default validation (validating against a single issuer value, as we do in line of business apps), 
                        // we inject our own multitenant validation logic
                        ValidateIssuer = true,
                        // If the app needs access to the entire organization, then add the logic
                        // of validating the Issuer here.
                        // IssuerValidator
                        ValidIssuers = tenantIds,
                        
                    },
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {   
                        SecurityTokenValidated = (context) =>
                        {
                            // If your authentication logic is based on users then add your logic here
                            return System.Threading.Tasks.Task.FromResult(0);
                        } ,                    
                        AuthenticationFailed = (context) =>
                        {
                            // Pass in the context back to the app
                            context.OwinContext.Response.Redirect("/Home/Error");
                            context.HandleResponse(); // Suppress the exception
                            return System.Threading.Tasks.Task.FromResult(0);
                        }
                    }
                });
        }
    }
}

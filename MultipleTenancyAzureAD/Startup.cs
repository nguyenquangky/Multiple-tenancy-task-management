using MultiTenancyAzureAD.Main.Extensions.Ninject;
using MultiTenancyAzureAD.Main.Services;
using Owin;

namespace MultiTenancyAzureAD.Main
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

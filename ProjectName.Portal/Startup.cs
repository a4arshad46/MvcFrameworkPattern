using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectName.Portal.Startup))]
namespace ProjectName.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

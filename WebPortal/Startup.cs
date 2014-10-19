using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebPortal.Startup))]
namespace WebPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bespoke.Web.Startup))]
namespace Bespoke.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

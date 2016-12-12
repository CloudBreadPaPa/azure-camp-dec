using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webjob_webapp.Startup))]
namespace webjob_webapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

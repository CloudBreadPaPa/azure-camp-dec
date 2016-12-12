using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cs_webapp_publish.Startup))]
namespace cs_webapp_publish
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AITECRM.Startup))]
namespace AITECRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

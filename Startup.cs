using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularAuthentication.Startup))]
namespace AngularAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

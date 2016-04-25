using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPSClientApp.Startup))]
namespace RPSClientApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

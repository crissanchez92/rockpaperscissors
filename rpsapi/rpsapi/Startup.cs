using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(rpsapi.Startup))]

namespace rpsapi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

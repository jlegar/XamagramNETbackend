using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XamagramNETbackend.Startup))]

namespace XamagramNETbackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}
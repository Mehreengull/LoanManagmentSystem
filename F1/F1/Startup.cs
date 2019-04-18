using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F1.Startup))]
namespace F1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

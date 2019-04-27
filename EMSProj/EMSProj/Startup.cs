using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMSProj.Startup))]
namespace EMSProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

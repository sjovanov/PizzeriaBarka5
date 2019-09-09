using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PicerijaBarka5.Startup))]
namespace PicerijaBarka5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

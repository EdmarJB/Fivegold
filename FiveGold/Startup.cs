using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FiveGold.Startup))]
namespace FiveGold
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

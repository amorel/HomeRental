using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeRental.Startup))]
namespace HomeRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

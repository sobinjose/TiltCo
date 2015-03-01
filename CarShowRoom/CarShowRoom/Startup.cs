using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarShowRoom.Startup))]
namespace CarShowRoom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MinesweeperProjectCLC247.Startup))]
namespace MinesweeperProjectCLC247
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

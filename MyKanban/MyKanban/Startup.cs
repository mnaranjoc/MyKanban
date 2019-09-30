using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyKanban.Startup))]
namespace MyKanban
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

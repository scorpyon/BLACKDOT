using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AskDanny.Startup))]
namespace AskDanny
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

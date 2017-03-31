using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twitter.Web.MVC.Startup))]
namespace Twitter.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

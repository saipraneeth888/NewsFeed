using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsArticle.Startup))]
namespace NewsArticle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

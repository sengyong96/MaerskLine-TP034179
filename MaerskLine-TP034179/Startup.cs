using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaerskLine_TP034179.Startup))]
namespace MaerskLine_TP034179
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

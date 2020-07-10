using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryApplication.Startup))]
namespace InventoryApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

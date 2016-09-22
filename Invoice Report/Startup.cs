using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Invoice_Report.Startup))]
namespace Invoice_Report
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

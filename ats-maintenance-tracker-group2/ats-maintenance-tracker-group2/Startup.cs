using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ats_maintenance_tracker_group2.Startup))]
namespace ats_maintenance_tracker_group2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

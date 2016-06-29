using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Collaborate_lrn_Py.Startup))]
namespace Collaborate_lrn_Py
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

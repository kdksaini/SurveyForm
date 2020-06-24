using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleTest.Startup))]
namespace SampleTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderFormMVC.Startup))]
namespace OrderFormMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}

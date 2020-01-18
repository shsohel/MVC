using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudMvcImageUpload.Startup))]
namespace CrudMvcImageUpload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

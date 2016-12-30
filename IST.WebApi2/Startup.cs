using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(IST.WebApi2.Startup))]

namespace IST.WebApi2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //To enable SignalR in your application
            //app.MapSignalR();
            //app.Map("/signalr", map =>
            //{
            //    map.UseCors(CorsOptions.AllowAll);
            //    var hubConfiguration = new HubConfiguration();

            //    map.RunSignalR(hubConfiguration);
            //});
            //To Enable CORS (cross-origin site requests)
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
            

        }
    }
}

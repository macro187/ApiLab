using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Meats.Compat;
using Meats.V2;

namespace Meats.Butcher.JsonRpc
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsonRpc();
            services.AddTransient<IButcher, Butcher>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseManualJsonRpc(config => {
                config.RegisterController<Butcher>("v2");
                config.RegisterController<ButcherV2ToV1Adapter>("v1");
            });
        }

    }
}

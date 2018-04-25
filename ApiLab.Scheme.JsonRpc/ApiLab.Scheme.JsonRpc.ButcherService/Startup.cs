using ApiLab.Normal.Butcher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ApiLab.Meats.V20;
using ApiLab.Meats.Compat;

namespace ApiLab.Scheme.JsonRpc.ButcherService
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsonRpc(options => {
                options.ShowServerExceptions = true;
            });

            services.AddTransient<NormalButcher>();
            services.AddTransient<IButcher, NormalButcher>();
            services.AddTransient<ButcherV2ToV1Adapter>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseManualJsonRpc(builder => {
                builder.RegisterController<NormalButcher>("v2");
                builder.RegisterController<ButcherV2ToV1Adapter>("v1");
            });
        }

    }
}

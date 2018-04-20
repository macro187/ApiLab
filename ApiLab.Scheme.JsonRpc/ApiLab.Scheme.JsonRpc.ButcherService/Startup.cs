using ApiLab.OneTrue.Butcher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ApiLab.Scheme.JsonRpc.ButcherService
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsonRpc(options => {
                options.ShowServerExceptions = true;
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseManualJsonRpc(builder => {
                builder.RegisterController<OneTrueButcher>();
            });
        }

    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Breads.Bakery.JsonRpc
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsonRpc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseManualJsonRpc(config => {
                config.RegisterController<Bakery>("v1");
            });
        }

    }
}

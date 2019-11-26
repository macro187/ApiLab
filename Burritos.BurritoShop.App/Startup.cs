using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Burritos.BurritoShop.App
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

    }
}

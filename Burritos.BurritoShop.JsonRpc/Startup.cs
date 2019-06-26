using System;
using Breads.V1;
using Meats.V2;
using MacroDotNetJsonRpcClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Burritos.BurritoShop.JsonRpc
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(
                    "AllowAny",
                    policy => {
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                        policy.AllowAnyOrigin();
                    });
            });

            services.AddJsonRpc();

            services.AddTransient(_ =>
                JsonRpcClient.Build<IBakery>(new Uri("http://localhost:53712/v1")));

            services.AddTransient(_ =>
                JsonRpcClient.Build<IButcher>(new Uri("http://localhost:53990/v2")));
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAny");

            app.UseManualJsonRpc(builder => {
                builder.RegisterController<BurritoShop>("v1");
            });
        }

    }
}

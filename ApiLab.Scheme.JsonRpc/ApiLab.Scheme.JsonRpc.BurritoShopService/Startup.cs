using System;
using ApiLab.Breads;
using ApiLab.Meats.V20;
using ApiLab.Normal.BurritoShop;
using ApiLab.Scheme.JsonRpc.Infrastructure.DotNetJsonRpcClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ApiLab.Scheme.JsonRpc.BurritoShopService
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

            services.AddJsonRpc(options => {
                options.ShowServerExceptions = true;
            });

            services.AddTransient(_ =>
                JsonRpcClient.Build<IBakery>(new Uri("http://localhost:53712/")));

            services.AddTransient(_ =>
                JsonRpcClient.Build<IButcher>(new Uri("http://localhost:53990/v2")));

            services.AddTransient<NormalBurritoShop>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAny");

            app.UseManualJsonRpc(builder => {
                builder.RegisterController<NormalBurritoShop>();
            });
        }

    }
}

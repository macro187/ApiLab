using System;
using ApiLab.Breads;
using ApiLab.Meats;
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
                    "FuckOffCors",
                    policy => {
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                        policy.AllowAnyOrigin();
                    });
            });

            services.AddJsonRpc(options => {
                options.ShowServerExceptions = true;
            });

            services.AddTransient<IBakery>(_ =>
                JsonRpcClient.Build<IBakery>(new Uri("http://localhost:53712/")));

            services.AddTransient<IButcher>(_ =>
                JsonRpcClient.Build<IButcher>(new Uri("http://localhost:53990/")));

            services.AddTransient<NormalBurritoShop>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("FuckOffCors");

            app.UseManualJsonRpc(builder => {
                builder.RegisterController<NormalBurritoShop>();
            });
        }

    }
}

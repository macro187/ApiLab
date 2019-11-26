using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Meats.Butcher.JsonRpc
{
    public class Program
    {

        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

    }
}

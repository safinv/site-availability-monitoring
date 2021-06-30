using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SiteAvailabilityMonitoring.Api.Extensions;

namespace SiteAvailabilityMonitoring.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().ApplyMigrate().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SiteAvailabilityMonitoring.Extensions;

namespace SiteAvailabilityMonitoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().ApplyMigrate().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
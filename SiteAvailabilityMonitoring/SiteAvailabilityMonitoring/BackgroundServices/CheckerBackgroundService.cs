using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using SiteAvailabilityMonitoring.Domain.Contracts;
using SiteAvailabilityMonitoring.Options;

namespace SiteAvailabilityMonitoring.BackgroundServices
{
    public class CheckerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CheckerBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(CheckWebsitesTaskAsync, (_scopeFactory, stoppingToken), stoppingToken,
                TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }
        
        private static async Task CheckWebsitesTaskAsync(object obj)
        {
            var (scopeFactory, stoppingToken) = ((IServiceScopeFactory, CancellationToken)) obj;
            using var scope = scopeFactory.CreateScope();

            var websiteManager = scope.ServiceProvider.GetRequiredService<IWebsiteManager>();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<CheckerOptions>>().Value;

            while (!stoppingToken.IsCancellationRequested)
            {
                await websiteManager.CheckAllOnAccessAndUpdate();
                await Task.Delay(options.DelayTimeSpan, stoppingToken);
            }
        }
    }
}
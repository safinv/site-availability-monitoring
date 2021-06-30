using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SiteAvailabilityMonitoring.Api.Options;
using SiteAvailabilityMonitoring.Domain.Commands;

namespace SiteAvailabilityMonitoring.Api.BackgroundServices
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

            var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<CheckerOptions>>().Value;

            while (!stoppingToken.IsCancellationRequested)
            {
                var command = new CheckAvailabilityCommand();
                await mediatr.Send(command);

                await Task.Delay(options.DelayTimeSpan, stoppingToken);
            }
        }
    }
}
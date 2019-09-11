using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Infrastructure.Services.Contracts;

namespace SiteAvailabilityMonitoring.HostedServices
{
    public class UrlChekerBackgroundService : BackgroundService
    {
        private readonly IDbQuery<Background> _backgroundQuery;
        private readonly IDbQuery<Site> _siteQuery;
        private readonly IDbRequest<Site> _siteCommand;
        private readonly ISiteAvailabilityCheker _siteAvailabilityCheker;

        public UrlChekerBackgroundService(
            IDbQuery<Background> backgroundQuery,
            IDbQuery<Site> siteQuery,
            IDbRequest<Site> siteCommand,
            ISiteAvailabilityCheker siteAvailabilityCheker)
        {
            _backgroundQuery = backgroundQuery ?? throw new ArgumentNullException(nameof(backgroundQuery));
            _siteQuery = siteQuery ?? throw new ArgumentNullException(nameof(siteQuery));
            _siteCommand = siteCommand ?? throw new ArgumentNullException(nameof(siteCommand));
            _siteAvailabilityCheker = siteAvailabilityCheker ?? throw new ArgumentNullException(nameof(siteAvailabilityCheker));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var time = await _backgroundQuery.GetAsync(b => b.Type == "Background");
                var span = new TimeSpan(time.Hour, time.Minutes, time.Seconds);

                var urls = await _siteQuery.GetAllAsync();

                try
                {
                    await CheckAsync(urls);
                }
                catch (Exception ex)
                {
                    //logs
                }

                await Task.Delay(span, stoppingToken).ConfigureAwait(false);
            }
        }

        private async Task CheckAsync(IEnumerable<Site> sites)
        {
            foreach (var site in sites)
            {
                await _siteAvailabilityCheker.CheckAsync(site);

                await _siteCommand.UpdateAsync(site);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Infrastructure.Repositories;
using SiteAvailabilityMonitoring.Infrastructure.Services.Contracts;

namespace SiteAvailabilityMonitoring.HostedServices
{
    public class UrlChekerBackgroundService : BackgroundService
    {
        private readonly BackgroundRepository _backgroundRepository;
        private readonly SiteRepository _siteRepository;
        private readonly ISiteAvailabilityCheker _siteAvailabilityCheker;

        public UrlChekerBackgroundService(BackgroundRepository backgroundRepository, SiteRepository siteRepository, ISiteAvailabilityCheker siteAvailabilityCheker)
        {
            _backgroundRepository = backgroundRepository ?? throw new ArgumentNullException(nameof(backgroundRepository));
            _siteRepository = siteRepository ?? throw new ArgumentNullException(nameof(siteRepository));
            _siteAvailabilityCheker = siteAvailabilityCheker ?? throw new ArgumentNullException(nameof(siteAvailabilityCheker));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var time = await _backgroundRepository.GetAsync(b => b.Type == "Background");
                var span = new TimeSpan(time.Hour, time.Minutes, time.Seconds);

                var urls = await _siteRepository.GetAllAsync();

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
                await _siteRepository.UpdateAsync(site);                
            }
        }
    }
}
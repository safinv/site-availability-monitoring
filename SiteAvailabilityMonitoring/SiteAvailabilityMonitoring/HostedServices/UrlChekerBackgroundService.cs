using Domain.Models;
using Infrastructure.Providers;
using Infrastructure.Services.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiteAvailabilityMonitoring.HostedServices
{
    public class UrlChekerBackgroundService : BackgroundService
    {
        private readonly BackgroundSettingsService _applicationSettingsService;
        private readonly UrlCollectionService _urlCollectionService;
        private readonly ISiteAvailabilityCheker _siteAvailabilityCheker;

        public UrlChekerBackgroundService(BackgroundSettingsService applicationSettingsService, UrlCollectionService urlCollectionService, ISiteAvailabilityCheker siteAvailabilityCheker)
        {
            _applicationSettingsService = applicationSettingsService ?? throw new ArgumentNullException(nameof(applicationSettingsService));
            _urlCollectionService = urlCollectionService;
            _siteAvailabilityCheker = siteAvailabilityCheker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var time = await _applicationSettingsService.GetBackgroundTimeAsync();                
                var span = new TimeSpan(time.Hour, time.Minutes, time.Seconds);

                var urls = await _urlCollectionService.GetAllAsync();

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

        private async Task CheckAsync(IEnumerable<UrlModel> urls)
        {
            foreach(var url in urls)
            {
                var result = await _siteAvailabilityCheker.CheckUrlAsync(url.Url);

                if(url.IsAvailable != result)
                {
                    url.IsAvailable = result;
                    await _urlCollectionService.UpdateAsync(url.Id, url);
                }
            }
        }
    }
}

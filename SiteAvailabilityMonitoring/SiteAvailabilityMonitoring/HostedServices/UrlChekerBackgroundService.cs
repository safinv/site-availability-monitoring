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
        private readonly BackgroundSettingService _backgroundSettingService;
        private readonly UrlService _urlService;
        private readonly ISiteAvailabilityCheker _siteAvailabilityCheker;

        public UrlChekerBackgroundService(BackgroundSettingService backgroundSettingService, UrlService urlService, ISiteAvailabilityCheker siteAvailabilityCheker)
        {
            _backgroundSettingService = backgroundSettingService ?? throw new ArgumentNullException(nameof(backgroundSettingService));
            _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
            _siteAvailabilityCheker = siteAvailabilityCheker ?? throw new ArgumentNullException(nameof(siteAvailabilityCheker));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var time = await _backgroundSettingService.GetBackgroundTimeAsync();                
                var span = new TimeSpan(time.Hour, time.Minutes, time.Seconds);

                var urls = await _urlService.GetAllAsync();

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
                    await _urlService.UpdateAsync(url.Id, url);
                }
            }
        }
    }
}

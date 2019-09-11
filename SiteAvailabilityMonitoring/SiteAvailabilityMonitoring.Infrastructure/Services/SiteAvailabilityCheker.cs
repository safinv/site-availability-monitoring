using System;
using System.Net.Http;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Infrastructure.Services.Contracts;

namespace SiteAvailabilityMonitoring.Infrastructure.Services
{
    public class SiteAvailabilityCheker : ISiteAvailabilityCheker
    {
        private readonly HttpClient _httpClient;

        public SiteAvailabilityCheker(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException();
        }

        public async Task CheckAsync(Site site)
        {
            var result = await CheckUrlAsync(site.Url);

            site.IsAvailable = result.IsAvailable;
            site.StatusCode = result.StatusCode;
        }

        private async Task<Response> CheckUrlAsync(string url)
        {
            try
            {
                using (var response = await _httpClient.GetAsync(url))
                {
                    return new Response { IsAvailable = response.IsSuccessStatusCode, StatusCode = response.StatusCode.ToString() };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsAvailable = false, StatusCode = ex.Message };
            }
        }

        private struct Response
        {
            public bool IsAvailable { get; set; }

            public string StatusCode { get; set; }
        }
    }
}
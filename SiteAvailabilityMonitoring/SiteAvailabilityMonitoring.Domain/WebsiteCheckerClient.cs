using System;
using System.Net.Http;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain
{
    public class WebsiteCheckerClient
    {
        private readonly HttpClient _httpClient;
        public WebsiteCheckerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Status> CheckAsync(string address)
        {
            try
            {
                var result = await _httpClient.GetAsync(address);
                return result.IsSuccessStatusCode ? Status.Enable : Status.Disable;
            }
            catch (Exception)
            {
                return Status.Disable;
            }
        }
    }
}
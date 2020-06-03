using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SiteAvailabilityMonitoring.Domain
{
    public class WebsiteCheckerClient
    {
        private readonly HttpClient _httpClient;
        public WebsiteCheckerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CheckAsync(string address)
        {
            try
            {
                var result = await _httpClient.GetAsync(address);
                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
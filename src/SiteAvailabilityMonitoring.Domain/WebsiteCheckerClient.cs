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

        public async Task<DbStatus> CheckAsync(string address)
        {
            try
            {
                var result = await _httpClient.GetAsync(address);
                return result.IsSuccessStatusCode ? DbStatus.Enable : DbStatus.Disable;
            }
            catch (Exception)
            {
                return DbStatus.Disable;
            }
        }
    }
}
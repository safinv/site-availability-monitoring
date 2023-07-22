using System;
using System.Net.Http;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Domain;

public class CheckWebsiteAvailabilityClient
{
    private readonly HttpClient _httpClient;

    public CheckWebsiteAvailabilityClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WebsiteAvailability> CheckAsync(string address)
    {
        try
        {
            var result = await _httpClient.GetAsync(address);
            return WebsiteAvailability.Ok(result);
        }
        catch (Exception)
        {
            return WebsiteAvailability.Error();
        }
    }
}
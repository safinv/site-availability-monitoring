using System.Net.Http;

namespace SiteAvailabilityMonitoring.Domain.Models;

public struct WebsiteAvailability
{
    public bool Available { get; private set; }
    public int StatusCode { get; private set; }

    public static WebsiteAvailability Ok(HttpResponseMessage result)
    {
        return new WebsiteAvailability
        {
            Available = result.IsSuccessStatusCode,
            StatusCode = (int)result.StatusCode
        };
    }

    public static WebsiteAvailability Error()
    {
        return new WebsiteAvailability
        {
            Available = false,
            StatusCode = 0
        };
    }
}
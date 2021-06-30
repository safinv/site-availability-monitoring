using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Abstractions.Dto
{
    public class WebsiteEdit
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("address")] public string Address { get; set; }
    }
}
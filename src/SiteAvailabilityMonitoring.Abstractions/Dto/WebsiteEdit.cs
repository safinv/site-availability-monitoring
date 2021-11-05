using System.Text.Json.Serialization;

namespace SiteAvailabilityMonitoring.Abstractions.Dto
{
    public class WebsiteEdit
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("address")] public string Address { get; set; }
    }
}
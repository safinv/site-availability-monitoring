using System.Text.Json.Serialization;

namespace SiteAvailabilityMonitoring.Abstractions.Dto
{
    public class Website
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("address")] public string Address { get; set; }

        [JsonPropertyName("available")] public bool Available { get; set; }

        [JsonPropertyName("status_code")] public int StatusCode { get; set; }
    }
}
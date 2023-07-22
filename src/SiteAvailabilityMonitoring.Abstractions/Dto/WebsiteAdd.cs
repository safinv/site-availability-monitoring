using System.Text.Json.Serialization;

namespace SiteAvailabilityMonitoring.Abstractions.Dto;

public sealed class WebsiteAdd
{
    [JsonPropertyName("address")] public string Address { get; set; }
}
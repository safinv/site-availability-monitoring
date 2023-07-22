using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiteAvailabilityMonitoring.Abstractions.Dto;

public class WebsiteList
{
    [JsonPropertyName("websites")] public IReadOnlyCollection<Website> Websites { get; set; }

    [JsonPropertyName("count")] public int Count { get; set; }
}
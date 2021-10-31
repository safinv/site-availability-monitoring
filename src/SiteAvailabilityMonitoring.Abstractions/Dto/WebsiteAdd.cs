using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiteAvailabilityMonitoring.Abstractions.Dto
{
    public sealed class WebsiteAdd
    {
        [JsonPropertyName("addresses")]
        public IReadOnlyCollection<string> Addresses { get; set; }
    }
}
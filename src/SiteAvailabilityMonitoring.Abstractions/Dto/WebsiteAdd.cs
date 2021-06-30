using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Abstractions.Dto
{
    public sealed class WebsiteAdd
    {
        [JsonProperty("addresses")]
        public List<string> Addresses { get; set; }
    }
}
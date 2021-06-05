using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public sealed class CreateWebsite
    {
        [JsonProperty("addresses")]
        public List<string> Addresses { get; set; }
    }
}
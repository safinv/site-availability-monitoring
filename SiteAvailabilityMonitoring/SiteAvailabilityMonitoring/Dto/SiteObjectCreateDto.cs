using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public sealed class SiteObjectCreateDto
    {
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
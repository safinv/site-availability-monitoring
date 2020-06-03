using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public sealed class WebsiteObjectCreateDto
    {
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
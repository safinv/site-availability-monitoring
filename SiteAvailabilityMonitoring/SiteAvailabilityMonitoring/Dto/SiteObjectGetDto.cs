using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public class SiteObjectGetDto
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
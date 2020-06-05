using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public class WebsiteObjectGetDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
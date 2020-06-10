using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public class WebsiteObjectEditDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
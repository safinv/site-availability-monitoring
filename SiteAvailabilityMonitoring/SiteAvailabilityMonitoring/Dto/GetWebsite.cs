using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Dto
{
    public class GetWebsite
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
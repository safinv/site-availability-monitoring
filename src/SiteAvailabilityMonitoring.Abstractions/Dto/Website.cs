using Newtonsoft.Json;

namespace SiteAvailabilityMonitoring.Abstractions.Dto
{
    public class Website
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
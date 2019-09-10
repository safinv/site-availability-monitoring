namespace SiteAvailabilityMonitoring.Domain.Models
{
    public class Site : BaseEntity
    {
        public string Url { get; set; }

        public bool IsAvailable { get; set; }

        public string StatusCode { get; set; }
    }
}
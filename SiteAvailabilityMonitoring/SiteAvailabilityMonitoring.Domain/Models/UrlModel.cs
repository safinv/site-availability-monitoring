namespace SiteAvailabilityMonitoring.Domain.Models
{
    public class UrlModel : BaseDbModel
    {
        public string Url { get; set; }

        public bool IsAvailable { get; set; }
    }
}
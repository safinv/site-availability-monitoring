namespace SiteAvailabilityMonitoring.Domain.Models
{
    public class Background : BaseEntity
    {
        public string Type { get; set; }

        public int Hour { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }
    }
}
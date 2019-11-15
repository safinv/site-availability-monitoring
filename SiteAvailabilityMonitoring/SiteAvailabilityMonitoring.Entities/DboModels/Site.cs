namespace SiteAvailabilityMonitoring.Entities.DboModels
{
    public class Site
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}
namespace SiteAvailabilityMonitoring.Entities
{
    public class DbWebsite
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public DbStatus Status { get; set;}

        public string StatusAsString => Status.ToString();
    }
}
namespace SiteAvailabilityMonitoring.Domain.Entities;

public class DbWebsite
    : Entity
{
    public long Id { get; set; }
    public string Address { get; set; }
    public bool Available { get; set; }
    public int StatusCode { get; set; }
}
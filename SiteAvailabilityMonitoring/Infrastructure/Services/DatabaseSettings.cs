using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Services
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
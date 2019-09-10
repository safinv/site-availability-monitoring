using SiteAvailabilityMonitoring.Domain.Database;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Repositories
{
    public class SiteRepository : BaseRepository<Site>
    {
        public SiteRepository(IDatabaseSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
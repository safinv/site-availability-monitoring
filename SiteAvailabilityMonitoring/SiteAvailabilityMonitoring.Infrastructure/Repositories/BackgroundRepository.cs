using SiteAvailabilityMonitoring.Domain.Database;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Repositories
{
    public class BackgroundRepository : BaseRepository<Background>
    {
        public BackgroundRepository(IDatabaseSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
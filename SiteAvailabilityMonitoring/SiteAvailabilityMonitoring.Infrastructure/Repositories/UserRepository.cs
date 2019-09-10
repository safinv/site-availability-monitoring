using SiteAvailabilityMonitoring.Domain.Database;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IDatabaseSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
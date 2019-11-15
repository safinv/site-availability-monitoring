using System.Collections.Generic;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Entities.DboModels;

namespace SiteAvailabilityMonitoring.Domain.DataAccessPoint
{
    public interface ISiteRepository
    {
        Task<IEnumerable<Site>> GetAllAsync();
        
        Task CreateAsync(Site site);
        
        Task UpdateAsync(Site site);
    }
}
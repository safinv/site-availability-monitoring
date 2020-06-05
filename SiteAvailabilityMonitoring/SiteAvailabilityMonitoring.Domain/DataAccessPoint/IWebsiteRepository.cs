using System.Collections.Generic;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain.DataAccessPoint
{
    public interface IWebsiteRepository
    {
        Task<IEnumerable<Website>> GetAllAsync();
        
        Task CreateAsync(Website website);
        
        Task UpdateAsync(Website website);
    }
}
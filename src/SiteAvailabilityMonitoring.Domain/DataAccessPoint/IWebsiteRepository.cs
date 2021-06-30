using System.Collections.Generic;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain.DataAccessPoint
{
    public interface IWebsiteRepository
    {
        Task<IEnumerable<DbWebsite>> GetAllAsync();
        
        Task<DbWebsite> CreateAsync(DbWebsite dbWebsite);
        
        Task UpdateAsync(DbWebsite dbWebsite);
    }
}
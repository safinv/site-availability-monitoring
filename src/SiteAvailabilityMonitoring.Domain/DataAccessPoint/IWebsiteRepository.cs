using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.DataAccessPoint
{
    public interface IWebsiteRepository
    {
        Task<DbWebsite> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<IEnumerable<DbWebsite>> GetAllAsync(
            CancellationToken cancellationToken = default);
        
        Task<IEnumerable<DbWebsite>> GetAsync(
            int limit = 10, 
            int offset = 0,
            CancellationToken cancellationToken = default);

        Task<DbWebsite> CreateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken);

        Task UpdateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken);

        Task DeleteAsync(long id, CancellationToken cancellationToken);

        Task<bool> AddressIsExist(string address, CancellationToken cancellationToken);
        
        Task<int> GetCount(CancellationToken cancellationToken);
    }
}
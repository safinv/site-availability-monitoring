using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.DataAccessPoint
{
    public interface IWebsiteRepository
    {
        Task<DbWebsite> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<IEnumerable<DbWebsite>> GetAsync(CancellationToken cancellationToken);

        Task<DbWebsite> CreateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken);

        Task UpdateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken);

        Task DeleteAsync(long id, CancellationToken cancellationToken);

        Task<bool> AddressIsExist(string address, CancellationToken cancellationToken);
    }
}
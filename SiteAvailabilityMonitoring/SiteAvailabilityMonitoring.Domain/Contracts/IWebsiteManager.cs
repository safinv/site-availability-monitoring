using System.Collections.Generic;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Entities.DbModels;

namespace SiteAvailabilityMonitoring.Domain.Contracts
{
    public interface IWebsiteManager
    {
        Task<IEnumerable<Website>> GetAllAsync();

        Task CreateAsync(string address);

        Task CheckOnAccessAndUpdate();
    }
}
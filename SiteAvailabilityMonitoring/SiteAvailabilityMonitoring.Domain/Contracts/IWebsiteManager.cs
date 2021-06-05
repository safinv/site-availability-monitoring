using System.Collections.Generic;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain.Contracts
{
    public interface IWebsiteManager
    {
        Task<IEnumerable<Website>> GetAllAsync();

        Task CreateAsync(List<string> addresses);

        Task EditAsync(long id, string address);

        Task CheckAllOnAccessAndUpdate();
    }
}
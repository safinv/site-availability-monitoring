using System.Collections.Generic;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Entities.Models;

namespace SiteAvailabilityMonitoring.Domain
{
    public interface ISiteManager
    {
        Task<IEnumerable<SiteModel>> GetAllAsync();

        Task CreateAsync(SiteModel model);

        Task CheckOnAccessAndUpdate();
    }
}
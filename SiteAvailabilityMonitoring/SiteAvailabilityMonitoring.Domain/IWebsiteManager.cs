using System.Collections.Generic;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Entities.Models;

namespace SiteAvailabilityMonitoring.Domain
{
    public interface IWebsiteManager
    {
        Task<IEnumerable<WebsiteModel>> GetAllAsync();

        Task CreateAsync(WebsiteModel websiteModel);

        Task CheckOnAccessAndUpdate();
    }
}
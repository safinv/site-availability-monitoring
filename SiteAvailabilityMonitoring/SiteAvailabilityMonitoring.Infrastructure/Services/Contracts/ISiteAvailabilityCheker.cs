using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Infrastructure.Services.Contracts
{
    public interface ISiteAvailabilityCheker
    {
        Task CheckAsync(Site site);
    }
}
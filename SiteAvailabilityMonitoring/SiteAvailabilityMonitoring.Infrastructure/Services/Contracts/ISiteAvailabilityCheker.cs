using System.Threading.Tasks;

namespace SiteAvailabilityMonitoring.Infrastructure.Services.Contracts
{
    public interface ISiteAvailabilityCheker
    {
        Task<bool> CheckUrlAsync(string url);
    }
}
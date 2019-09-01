using System.Threading.Tasks;

namespace Infrastructure.Services.Contracts
{
    public interface ISiteAvailabilityCheker
    {
        Task<bool> CheckUrlAsync(string url);
    }
}

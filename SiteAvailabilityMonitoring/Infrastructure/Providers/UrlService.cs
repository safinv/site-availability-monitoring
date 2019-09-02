using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.Database;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Providers
{
    public class UrlService : BaseDatabaseService<UrlModel>
    {
        public UrlService(IDatabaseSettings settings) : base(settings)
        {
        }

        protected override string CollectionName => "Urls";

        public async Task<UrlModel> TryGetById(string id)
        {
            return await GetAsync(model => model.Id == id);
        }
    }
}
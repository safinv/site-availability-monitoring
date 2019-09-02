using System.Linq;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.Database;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Providers
{
    public class BackgroundSettingService : BaseDatabaseService<BackgroundSettingModel>
    {
        private const string BackgroundType = "Background";

        public BackgroundSettingService(IDatabaseSettings settings) : base(settings)
        {
        }

        protected override string CollectionName => "ApplicationSettings";

        public async Task<BackgroundSettingModel> GetBackgroundTimeAsync()
        {
            var model = await WhereAsync(x => x.Type == BackgroundType);
            return model.FirstOrDefault();
        }
    }
}
using Domain.Database;
using Domain.Models;
using Domain.Settings;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class BackgroundSettingsService : BaseDatabaseService<BackgroundSettingModel>
    {
        private const string BackgroundType = "Background";
        private const string BackgroundCollectionName = "ApplicationSettings";

        public BackgroundSettingsService(IDatabaseSettings settings) : base(settings)
        {
        }

        public async Task<BackgroundSettingModel> GetBackgroundTimeAsync()
        {
            var model = await GetAllAsync(x => x.Type == BackgroundType);
            return model.FirstOrDefault();
        }

        protected override string GetCollectionName()
        {
            return BackgroundCollectionName;
        }
    }
}

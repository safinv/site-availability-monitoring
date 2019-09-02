using Domain.Database;
using Domain.Models;
using Domain.Settings;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Providers
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
            var model = await GetAllAsync(x => x.Type == BackgroundType);
            return model.FirstOrDefault();
        }
    }
}

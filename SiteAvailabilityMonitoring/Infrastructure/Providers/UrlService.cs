using Domain.Settings;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Database;

namespace Infrastructure.Providers
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

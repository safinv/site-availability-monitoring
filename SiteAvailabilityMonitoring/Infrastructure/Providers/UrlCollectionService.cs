using Domain.Settings;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Database;

namespace Infrastructure.Providers
{
    public class UrlCollectionService : BaseDatabaseService<UrlModel>
    {
        public UrlCollectionService(IDatabaseSettings settings) : base(settings)
        {
        }

        public async Task<UrlModel> TryGetById(string id)
        {
            return await GetAsync(model => model.Id == id);
        } 

        protected override string GetCollectionName()
        {
            return "Urls";
        }
    }
}

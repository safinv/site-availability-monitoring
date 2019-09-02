using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.Database;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Infrastructure.Providers
{
    public class UserService : BaseDatabaseService<UserModel>
    {
        public UserService(IDatabaseSettings settings) : base(settings)
        {
        }

        protected override string CollectionName => "Users";

        public async Task<UserModel> TryGetUser(string login, string password)
        {
            var result = await GetAsync(user => user.Login == login && user.Password == password);
            return result;
        }

        public async Task<UserModel> TryGetUser(string login)
        {
            var result = await GetAsync(user => user.Login == login);
            return result;
        }
    }
}
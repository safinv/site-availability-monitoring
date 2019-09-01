using Domain.Database;
using Domain.Models;
using Domain.Settings;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class UserService : BaseDatabaseService<UserModel>
    {
        public UserService(IDatabaseSettings settings) : base(settings)
        {
        }

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

        protected override string GetCollectionName()
        {
            return "Users";
        }
    }
}

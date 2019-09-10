using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.DatabaseName);
            }
        }

        public IMongoCollection<User> Collection
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
    }
}
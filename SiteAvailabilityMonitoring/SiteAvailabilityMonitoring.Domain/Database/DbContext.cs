using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class DbContext<TEntity>
    {
        private readonly IMongoDatabase _database = null;

        public DbContext(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(dbSettings.DatabaseName);
            }
        }

        public IMongoCollection<TEntity> Collection => _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}
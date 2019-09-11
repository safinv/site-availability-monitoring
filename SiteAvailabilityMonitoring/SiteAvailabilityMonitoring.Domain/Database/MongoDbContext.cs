using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class MongoDbContext<TEntity>
    {
        private readonly IMongoDatabase _database = null;
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> _commands;

        public MongoDbContext(IDatabaseSettings dbSettings)
        {
            MongoClient = new MongoClient(dbSettings.ConnectionString);
            if (MongoClient != null)
            {
                _database = MongoClient.GetDatabase(dbSettings.DatabaseName);
            }

            _commands = new List<Func<Task>>();
        }

        public IMongoCollection<TEntity> Collection => _database.GetCollection<TEntity>(typeof(TEntity).Name);

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
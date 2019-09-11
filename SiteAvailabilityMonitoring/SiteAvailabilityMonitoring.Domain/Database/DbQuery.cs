using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class DbQuery<TEntity> : IDbQuery<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoDatabase _database;
               
        public DbQuery(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(dbSettings.DatabaseName);
            }
        }

        public IMongoCollection<TEntity> Collection => _database.GetCollection<TEntity>(typeof(TEntity).Name);
               
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await Collection.FindAsync(_ => true);
            return result.ToList();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await Collection.FindAsync(expression);
            return result.FirstOrDefault();
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await Collection.InsertOneAsync(obj);
        }

        public virtual async Task RemoveAsync(string id)
        {
            await Collection.DeleteOneAsync(entity => entity.Id == id);
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await Collection.ReplaceOneAsync(entity => entity.Id == obj.Id, obj);
        }
    }
}
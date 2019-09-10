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
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<TEntity> _dbSet;

        protected BaseRepository(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(dbSettings.DatabaseName);
            }

            _dbSet = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await _dbSet.InsertOneAsync(obj);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _dbSet.FindAsync(_ => true);
            return result.ToList();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await _dbSet.FindAsync(expression);
            return result.FirstOrDefault();
        }

        public virtual async Task RemoveAsync(string id)
        {
            await _dbSet.DeleteOneAsync(entity => entity.Id == id);
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await _dbSet.ReplaceOneAsync(entity => entity.Id == obj.Id, obj);
        }
    }
}
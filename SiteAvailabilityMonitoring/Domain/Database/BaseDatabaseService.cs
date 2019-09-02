using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public abstract class BaseDatabaseService<T> where T : BaseDbModel
    {
        private readonly IMongoCollection<T> _models;

        protected abstract string CollectionName { get; }

        public BaseDatabaseService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _models = database.GetCollection<T>(CollectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = await _models.FindAsync(model => true);
            return result.ToList();
        }

        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _models.FindAsync(expression);
            return result.ToList();
        }

        public async Task<T> GetAsync(string id)
        {
            var result = await _models.FindAsync<T>(model => model.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _models.FindAsync<T>(expression);
            return result.FirstOrDefault();
        }

        public async Task<T> CreateAsync(T model)
        {
            await _models.InsertOneAsync(model);
            return model;
        }

        public async Task RemoveAsync(string id)
        {
            await _models.DeleteOneAsync(model => model.Id == id);
        }

        public async Task UpdateAsync(string id, T updated)
        {
            await _models.ReplaceOneAsync(model => model.Id == id, updated);
        }
    }
}
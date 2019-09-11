using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class DbRequest<TEntity> : IDbRequest<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext<TEntity> _dbContext;

        public DbRequest(IDatabaseSettings dbSettings)
        {
            _dbContext = new DbContext<TEntity>(dbSettings);
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await _dbContext.Collection.InsertOneAsync(obj);
        }

        public virtual async Task RemoveAsync(string id)
        {
            await _dbContext.Collection.DeleteOneAsync(entity => entity.Id == id);
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await _dbContext.Collection.ReplaceOneAsync(entity => entity.Id == obj.Id, obj);
        }
    }
}
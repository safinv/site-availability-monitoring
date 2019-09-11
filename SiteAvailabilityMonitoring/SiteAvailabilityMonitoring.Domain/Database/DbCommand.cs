using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Domain.Settings;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class DbCommand<TEntity> : IDbCommand<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoDbContext<TEntity> _dbContext;

        public IDbQuery<TEntity> Query { get; private set; }

        public DbCommand(IDatabaseSettings dbSettings)
        {
            _dbContext = new MongoDbContext<TEntity>(dbSettings);
            Query = new DbQuery<TEntity>(_dbContext);
        }

        public virtual void Add(TEntity obj)
        {
            _dbContext.AddCommand(async () => await _dbContext.Collection.InsertOneAsync(obj));
        }

        public virtual void Remove(string id)
        {
            _dbContext.AddCommand(async () => await _dbContext.Collection.DeleteOneAsync(entity => entity.Id == id));
        }

        public virtual void Update(TEntity obj)
        {
            _dbContext.AddCommand(async () => await _dbContext.Collection.ReplaceOneAsync(entity => entity.Id == obj.Id, obj));
        }

        public async Task<bool> Commit()
        {
            return await _dbContext.SaveChanges() > 0;
        }
    }
}
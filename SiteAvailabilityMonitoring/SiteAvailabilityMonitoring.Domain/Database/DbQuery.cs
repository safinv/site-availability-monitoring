using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Domain.Database
{
    public class DbQuery<TEntity> : IDbQuery<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoDbContext<TEntity> _dbContext = null;

        public DbQuery(MongoDbContext<TEntity> dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _dbContext.Collection.FindAsync(_ => true);
            return result.ToList();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await _dbContext.Collection.FindAsync(expression);
            return result.FirstOrDefault();
        }
    }
}
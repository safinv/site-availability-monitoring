using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using MongoDB.Driver;

using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Domain.Database.Contracts
{
    public interface IDbQuery<TEntity> where TEntity : BaseEntity
    {
        IMongoCollection<TEntity> Collection { get; }

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task AddAsync(TEntity obj);

        Task RemoveAsync(string id);

        Task UpdateAsync(TEntity obj);
    }
}
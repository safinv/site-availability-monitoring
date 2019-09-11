using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Domain.Database.Contracts
{
    public interface IDbQuery<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    }
}
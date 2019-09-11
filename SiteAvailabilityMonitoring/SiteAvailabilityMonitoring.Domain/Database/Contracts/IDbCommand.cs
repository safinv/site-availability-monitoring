using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Domain.Database.Contracts
{
    public interface IDbCommand<TEntity> where TEntity : BaseEntity
    {
        IDbQuery<TEntity> Query { get; }

        void Add(TEntity obj);

        void Remove(string id);

        void Update(TEntity obj);

        Task<bool> Commit();
    }
}
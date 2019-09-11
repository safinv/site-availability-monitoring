using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Domain.Database.Contracts
{
    public interface IDbRequest<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity obj);

        Task RemoveAsync(string id);

        Task UpdateAsync(TEntity obj);
    }
}
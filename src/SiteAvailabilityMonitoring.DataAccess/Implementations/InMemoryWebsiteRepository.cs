using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.DataAccess.Implementations
{
    public class InMemoryWebsiteRepository
        : IWebsiteRepository
    {
        private readonly ConcurrentDictionary<long, DbWebsite> _websites = new();
        
        public Task<DbWebsite> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            _websites.TryGetValue(id, out var website);
            return Task.FromResult(website);
        }

        public Task<IEnumerable<DbWebsite>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DbWebsite>> GetAsync(int limit = 10, int offset = 0, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DbWebsite>> GetAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_websites.Values.Select(x => x));
        }

        public async Task<DbWebsite> CreateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken)
        {
            var id = GenerateId();
            dbWebsite.Id = id;
            _websites.TryAdd(id, dbWebsite);

            var website = await GetByIdAsync(id, cancellationToken);
            return website;
        }

        public Task UpdateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken)
        {
            _websites.TryUpdate(dbWebsite.Id, dbWebsite, dbWebsite);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            _websites.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<bool> AddressIsExist(string address, CancellationToken cancellationToken)
        {
            return Task.FromResult(_websites.Values.Any(x => x.Address == address));
        }

        public Task<int> GetCount(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private long GenerateId()
        {
            return _websites.Keys.Count + 1;
        }
    }
}
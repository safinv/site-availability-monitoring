using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper;

using SiteAvailabilityMonitoring.DataAccess.Base;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.DataAccess.Implementations
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public WebsiteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<DbWebsite>> GetAllAsync()
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"SELECT id, address, status FROM websites";
            var result = await connection.QueryAsync<DbWebsite>(query);
            
            return result;
        }

        public async Task<DbWebsite> CreateAsync(DbWebsite dbWebsite)
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"INSERT INTO websites (address, status) VALUES (@Address, @StatusAsString::e_status) RETURNING *";

            var dbDataReader = await connection.ExecuteReaderAsync(query, dbWebsite);
            return dbDataReader.Parse<DbWebsite>().FirstOrDefault();
        }

        public async Task UpdateAsync(DbWebsite dbWebsite)
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"UPDATE websites SET address = @Address, status = @StatusAsString::e_status WHERE id = @Id";

            await connection.ExecuteAsync(query, dbWebsite);
        }
    }
}
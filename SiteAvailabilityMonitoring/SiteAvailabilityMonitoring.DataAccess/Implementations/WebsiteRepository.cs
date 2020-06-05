using System.Collections.Generic;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.DataAccess.Implementations
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly string _connectionString;
        public WebsiteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Website>> GetAllAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var query = "SELECT id, address, status FROM websites";
            var result = await connection.QueryAsync<Website>(query);
            
            return result;
        }

        public async Task CreateAsync(Website website)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var query = "INSERT INTO websites (address, status) VALUES (@Address, @StatusAsString::e_status)";

            await connection.ExecuteAsync(query, website);
        }

        public async Task UpdateAsync(Website website)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var query = "UPDATE websites SET status = @StatusAsString::e_status WHERE id = @Id";

            await connection.ExecuteAsync(query, website);
        }
    }
}
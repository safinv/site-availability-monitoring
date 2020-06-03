using System.Collections.Generic;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities.DbModels;

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
            var query = "SELECT * FROM sites";
            var result = await connection.QueryAsync<Website>(query);
            
            return result;
        }

        public async Task CreateAsync(Website website)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var query = "INSERT INTO sites (address, status) VALUES (@Address, @Status)";

            await connection.ExecuteAsync(query, website);
        }

        public async Task UpdateAsync(Website website)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var query = "UPDATE sites SET status = @Status WHERE id = @Id";

            await connection.ExecuteAsync(query, website);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities.DboModels;

namespace SiteAvailabilityMonitoring.DataAccess.Implementations
{
    public class SiteRepository : ISiteRepository
    {
        private readonly string _connectionString;
        public SiteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Site>> GetAllAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sqlQuery = "SELECT * FROM Sites";
            var result = await connection.QueryAsync<Site>(sqlQuery);
            
            return result;
        }

        public async Task CreateAsync(Site site)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sqlQuery = "INSERT INTO sites (address) VALUES (@Address)";

            await connection.ExecuteAsync(sqlQuery, site);
        }

        public async Task UpdateAsync(Site site)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sqlQuery = "UPDATE sites SET status = @Status WHERE id = @Id";

            await connection.ExecuteAsync(sqlQuery, site);
        }
    }
}
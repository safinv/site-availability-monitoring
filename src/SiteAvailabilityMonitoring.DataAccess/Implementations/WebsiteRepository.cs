using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using SiteAvailabilityMonitoring.DataAccess.Base;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.DataAccess.Implementations
{
    public class WebsiteRepository
        : IWebsiteRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public WebsiteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<DbWebsite> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            if (id == 0) return null;
            
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"SELECT id, address, available, status_code FROM website WHERE id = {id}";
            var result = await connection.QueryFirstAsync<DbWebsite>(query, cancellationToken);

            return result;
        }

        public async Task<IEnumerable<DbWebsite>> GetAsync(CancellationToken cancellationToken)
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"SELECT id, address, available, status_code FROM website";
            var result = await connection.QueryAsync<DbWebsite>(query, cancellationToken);

            return result;
        }

        public async Task<DbWebsite> CreateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken)
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query =
                @$"INSERT INTO website (address, available, status_code) VALUES (@Address, @Available, @StatusCode) RETURNING *";

            var commandDefinition = new CommandDefinition(
                query,
                dbWebsite,
                commandType: CommandType.Text,
                cancellationToken: cancellationToken);
            
            var dbDataReader = await connection.ExecuteReaderAsync(commandDefinition);
            return dbDataReader.Parse<DbWebsite>().FirstOrDefault();
        }

        public async Task UpdateAsync(DbWebsite dbWebsite, CancellationToken cancellationToken)
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"UPDATE website SET address = @Address, available = @Available, status_code = @StatusCode WHERE id = @Id";

            var commandDefinition = new CommandDefinition(
                query,
                dbWebsite,
                commandType: CommandType.Text,
                cancellationToken: cancellationToken);

            await connection.ExecuteAsync(commandDefinition);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            await using var connection = _connectionFactory.CreateConnection();
            var query = @$"DELETE FROM website WHERE id = @id";

            var commandDefinition = new CommandDefinition(
                query,
                new {id},
                commandType: CommandType.Text,
                cancellationToken: cancellationToken);

            await connection.ExecuteAsync(commandDefinition);
        }
    }
}
using System.Data.Common;
using Npgsql;

namespace SiteAvailabilityMonitoring.DataAccess.Base
{
    internal class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteAvailabilityMonitoring.DataAccess.Base;
using SiteAvailabilityMonitoring.DataAccess.Migrations;

namespace SiteAvailabilityMonitoring.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NpgsqlDatabase");
            services.AddSingleton<IConnectionFactory>(
                new ConnectionFactory(connectionString));

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                {
                    builder.AddPostgres();
                    builder.WithGlobalConnectionString(connectionString);
                    builder.ScanIn(typeof(InitMigration).Assembly).For.Migrations();
                })
                .AddLogging(builder => builder.AddFluentMigratorConsole());

            return services;
        }
    }
}
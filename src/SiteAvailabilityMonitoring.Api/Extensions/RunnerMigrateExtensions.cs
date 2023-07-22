using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SiteAvailabilityMonitoring.Api.Extensions;

public static class RunnerMigrateExtensions
{
    public static IHost ApplyMigrate(this IHost host)
    {
        host.Services.Migrate();
        return host;
    }

    private static void Migrate(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        if (runner.HasMigrationsToApplyUp())
        {
            runner.MigrateUp();
        }
        else
        {
            Console.WriteLine("No migrations to apply...");
            Console.WriteLine();
        }
    }
}
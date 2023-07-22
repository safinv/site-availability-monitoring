using Microsoft.Extensions.DependencyInjection;

namespace SiteAvailabilityMonitoring.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(x => { x.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly); });
        return services;
    }
}
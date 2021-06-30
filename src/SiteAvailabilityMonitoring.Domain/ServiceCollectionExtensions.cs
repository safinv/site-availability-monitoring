using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SiteAvailabilityMonitoring.Domain.Queries;

namespace SiteAvailabilityMonitoring.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(WebsiteGetQuery));
            return services;
        }
    }
}
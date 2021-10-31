using System.Collections.Generic;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class GetWebsitesQuery
        : IRequest<IReadOnlyCollection<Website>>
    {
    }
}
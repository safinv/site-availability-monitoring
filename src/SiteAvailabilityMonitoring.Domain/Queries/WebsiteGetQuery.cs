using System.Collections.Generic;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class WebsiteGetQuery
        : IRequest<IEnumerable<Website>>
    {
    }
}
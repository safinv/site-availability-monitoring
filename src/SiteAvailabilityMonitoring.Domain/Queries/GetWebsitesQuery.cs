using System.Collections.Generic;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class GetWebsitesQuery
        : IRequest<IReadOnlyCollection<Website>>
    {
        public int Limit { get; }
        public int Offset { get; }

        public GetWebsitesQuery(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;
        }
    }
}
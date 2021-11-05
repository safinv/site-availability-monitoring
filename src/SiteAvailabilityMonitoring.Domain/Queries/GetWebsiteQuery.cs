using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class GetWebsiteQuery
        : IRequest<Website>
    {
        public long Id { get; }

        public GetWebsiteQuery(long id)
        {
            Id = id;
        }
    }
}
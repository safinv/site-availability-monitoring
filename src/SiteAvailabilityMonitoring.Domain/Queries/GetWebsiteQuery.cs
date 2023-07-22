using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Queries;

public class GetWebsiteQuery
    : IRequest<Website>
{
    public GetWebsiteQuery(long id)
    {
        Id = id;
    }

    public long Id { get; }
}
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Queries;

public class GetWebsitesQuery
    : IRequest<WebsiteList>
{
    public GetWebsitesQuery(int limit, int offset)
    {
        Limit = limit;
        Offset = offset;
    }

    public int Limit { get; }
    public int Offset { get; }
}
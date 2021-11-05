using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class GetWebsiteQueryHandler
        : IRequestHandler<GetWebsiteQuery, Website>
    {
        private readonly IWebsiteRepository _websiteRepository;

        public GetWebsiteQueryHandler(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task<Website> Handle(
            GetWebsiteQuery query,
            CancellationToken cancellationToken)
        {
            var website = await _websiteRepository.GetByIdAsync(query.Id, cancellationToken);
            return website.Adapt<Website>();
        }
    }
}
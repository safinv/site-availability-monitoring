using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class GetWebsitesQueryHandler
        : IRequestHandler<GetWebsitesQuery, IReadOnlyCollection<Website>>
    {
        private readonly IWebsiteRepository _websiteRepository;

        public GetWebsitesQueryHandler(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task<IReadOnlyCollection<Website>> Handle(
            GetWebsitesQuery request, 
            CancellationToken cancellationToken)
        {
            var websites = await _websiteRepository.GetAsync(cancellationToken);
            return websites.OrderBy(x => x.Id).Adapt<IReadOnlyCollection<Website>>();
        }
    }
}
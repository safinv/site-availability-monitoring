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
        : IRequestHandler<GetWebsitesQuery, WebsiteList>
    {
        private readonly IWebsiteRepository _websiteRepository;

        public GetWebsitesQueryHandler(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task<WebsiteList> Handle(
            GetWebsitesQuery request,
            CancellationToken cancellationToken)
        {
            var websites = await _websiteRepository
                .GetAsync(request.Limit, request.Offset, cancellationToken);

            var count = await _websiteRepository.GetCount(cancellationToken);

            var websitesDto = websites
                .OrderBy(x => x.Id)
                .Adapt<IReadOnlyCollection<Website>>();

            return new WebsiteList
            {
                Websites = websitesDto,
                Count = count
            };
        }
    }
}
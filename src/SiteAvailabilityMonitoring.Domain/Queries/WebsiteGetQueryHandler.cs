using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring.Domain.Queries
{
    public class WebsiteGetQueryHandler
        : IRequestHandler<WebsiteGetQuery, IEnumerable<Website>>
    {
        private readonly IWebsiteRepository _websiteRepository;

        public WebsiteGetQueryHandler(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task<IEnumerable<Website>> Handle(WebsiteGetQuery request, CancellationToken cancellationToken)
        {
            var sites = await _websiteRepository.GetAllAsync();
            return sites.Select(x => new Website
            {
                Id = x.Id,
                Address = x.Address,
                Status = x.StatusAsString
            });
        }
    }
}
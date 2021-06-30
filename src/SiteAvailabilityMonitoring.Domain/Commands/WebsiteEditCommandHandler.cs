using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class WebsiteEditCommandHandler 
        : IRequestHandler<WebsiteEditCommand>
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly WebsiteCheckerClient _websiteCheckerClient;

        public WebsiteEditCommandHandler(IWebsiteRepository websiteRepository, WebsiteCheckerClient websiteCheckerClient)
        {
            _websiteRepository = websiteRepository;
            _websiteCheckerClient = websiteCheckerClient;
        }

        public async Task<Unit> Handle(WebsiteEditCommand request, CancellationToken cancellationToken)
        {
            var status = await _websiteCheckerClient.CheckAsync(request.Address);
            var website = new DbWebsite {Id = request.Id, Address = request.Address, Status = status};

            await _websiteRepository.UpdateAsync(website);
            
            return Unit.Value;
        }
    }
}
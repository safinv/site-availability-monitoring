using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class WebsiteDeleteCommandHandler
        : IRequestHandler<WebsiteDeleteCommand>
    {
        private readonly IWebsiteRepository _websiteRepository;

        public WebsiteDeleteCommandHandler(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task<Unit> Handle(WebsiteDeleteCommand request, CancellationToken cancellationToken)
        {
            await _websiteRepository.DeleteAsync(request.Id);
            
            return Unit.Value;
        }
    }
}
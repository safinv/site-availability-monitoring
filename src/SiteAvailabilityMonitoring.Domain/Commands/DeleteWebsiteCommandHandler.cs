using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class DeleteWebsiteCommandHandler
        : IRequestHandler<DeleteWebsiteCommand>
    {
        private readonly IWebsiteRepository _websiteRepository;

        public DeleteWebsiteCommandHandler(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async Task<Unit> Handle(DeleteWebsiteCommand request, CancellationToken cancellationToken)
        {
            await _websiteRepository.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
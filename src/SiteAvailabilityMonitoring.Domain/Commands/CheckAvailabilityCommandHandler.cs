using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class CheckAvailabilityCommandHandler
        : IRequestHandler<CheckAvailabilityCommand>
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly CheckWebsiteAvailabilityClient _checkWebsiteAvailabilityClient;

        public CheckAvailabilityCommandHandler(
            IWebsiteRepository websiteRepository,
            CheckWebsiteAvailabilityClient checkWebsiteAvailabilityClient)
        {
            _websiteRepository = websiteRepository;
            _checkWebsiteAvailabilityClient = checkWebsiteAvailabilityClient;
        }

        public async Task<Unit> Handle(CheckAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var websites = await _websiteRepository.GetAsync(cancellationToken);
            var tasks = websites
                .Select(ws => UpdateStatus(ws, cancellationToken))
                .ToList();

            await Task.WhenAll(tasks);
            return Unit.Value;
        }

        private async Task UpdateStatus(DbWebsite website, CancellationToken cancellationToken)
        {
            var availability = await _checkWebsiteAvailabilityClient.CheckAsync(website.Address);
            if (availability.StatusCode != website.StatusCode)
            {
                website.StatusCode = availability.StatusCode;
                website.Available = availability.Available;
                
                await _websiteRepository.UpdateAsync(website, cancellationToken);
            }
        }
    }
}
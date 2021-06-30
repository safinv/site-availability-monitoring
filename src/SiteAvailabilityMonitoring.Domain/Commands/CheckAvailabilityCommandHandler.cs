using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class CheckAvailabilityCommandHandler
        : IRequestHandler<CheckAvailabilityCommand>
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly WebsiteCheckerClient _websiteCheckerClient;

        public CheckAvailabilityCommandHandler(
            IWebsiteRepository websiteRepository,
            WebsiteCheckerClient websiteCheckerClient)
        {
            _websiteRepository = websiteRepository;
            _websiteCheckerClient = websiteCheckerClient;
        }

        public async Task<Unit> Handle(CheckAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var websites = await _websiteRepository.GetAllAsync();
            var tasks = websites.Select(UpdateStatus).ToList();

            await Task.WhenAll(tasks);

            return Unit.Value;
        }

        private async Task UpdateStatus(DbWebsite website)
        {
            var isAccessed = await _websiteCheckerClient.CheckAsync(website.Address);
            if (isAccessed != website.Status)
            {
                website.Status = isAccessed;
                await _websiteRepository.UpdateAsync(website);
            }
        }
    }
}
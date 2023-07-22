using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands;

public class CheckAvailabilityCommandHandler
    : IRequestHandler<CheckAvailabilityCommand>
{
    private readonly CheckWebsiteAvailabilityClient _checkWebsiteAvailabilityClient;
    private readonly ILogger<CheckAvailabilityCommandHandler> _logger;
    private readonly IWebsiteRepository _websiteRepository;

    public CheckAvailabilityCommandHandler(
        IWebsiteRepository websiteRepository,
        CheckWebsiteAvailabilityClient checkWebsiteAvailabilityClient,
        ILogger<CheckAvailabilityCommandHandler> logger)
    {
        _websiteRepository = websiteRepository;
        _checkWebsiteAvailabilityClient = checkWebsiteAvailabilityClient;
        _logger = logger;
    }

    public async Task Handle(CheckAvailabilityCommand request, CancellationToken cancellationToken)
    {
        var affectedCount = 0;
        var count = await _websiteRepository.GetCount(cancellationToken);

        while (affectedCount < count)
        {
            var websites = (await _websiteRepository
                .GetAsync(cancellationToken: cancellationToken)).ToList();

            var tasks = websites
                .Select(ws => UpdateStatus(ws, cancellationToken))
                .ToList();
            await Task.WhenAll(tasks);
            affectedCount += affectedCount + websites.Count;

            _logger.LogInformation($"Affected websites: {affectedCount}.");
        }
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
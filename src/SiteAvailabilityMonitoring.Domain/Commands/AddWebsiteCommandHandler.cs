using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands;

public class AddWebsiteCommandHandler
    : IRequestHandler<AddWebsiteCommand, Website>
{
    private readonly CheckWebsiteAvailabilityClient _checkWebsiteAvailabilityClient;
    private readonly IWebsiteRepository _websiteRepository;

    public AddWebsiteCommandHandler(IWebsiteRepository websiteRepository,
        CheckWebsiteAvailabilityClient checkWebsiteAvailabilityClient)
    {
        _websiteRepository = websiteRepository;
        _checkWebsiteAvailabilityClient = checkWebsiteAvailabilityClient;
    }

    public async Task<Website> Handle(AddWebsiteCommand command, CancellationToken cancellationToken)
    {
        var clearAddress = command.Address.TrimEnd('/');

        var addressIsExist = await _websiteRepository.AddressIsExist(clearAddress, cancellationToken);
        if (addressIsExist) return null;

        var availability = await _checkWebsiteAvailabilityClient.CheckAsync(clearAddress);
        var website = new DbWebsite
        {
            Address = clearAddress,
            StatusCode = availability.StatusCode,
            Available = availability.Available
        };

        var dbWebsite = await _websiteRepository.CreateAsync(website, cancellationToken);

        return dbWebsite.Adapt<Website>();
    }
}
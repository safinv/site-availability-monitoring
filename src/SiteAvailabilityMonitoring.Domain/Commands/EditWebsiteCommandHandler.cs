using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands;

public class EditWebsiteCommandHandler
    : IRequestHandler<EditWebsiteCommand, Website>
{
    private readonly CheckWebsiteAvailabilityClient _checkWebsiteAvailabilityClient;
    private readonly IWebsiteRepository _websiteRepository;

    public EditWebsiteCommandHandler(IWebsiteRepository websiteRepository,
        CheckWebsiteAvailabilityClient checkWebsiteAvailabilityClient)
    {
        _websiteRepository = websiteRepository;
        _checkWebsiteAvailabilityClient = checkWebsiteAvailabilityClient;
    }

    public async Task<Website> Handle(EditWebsiteCommand request, CancellationToken cancellationToken)
    {
        var availability = await _checkWebsiteAvailabilityClient.CheckAsync(request.Address);
        var website = new DbWebsite
        {
            Id = request.Id,
            Address = request.Address,
            StatusCode = availability.StatusCode,
            Available = availability.Available
        };

        await _websiteRepository.UpdateAsync(website, cancellationToken);

        var updatedWebsite = await _websiteRepository.GetByIdAsync(request.Id, cancellationToken);
        return updatedWebsite.Adapt<Website>();
    }
}
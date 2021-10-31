using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Domain.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class AddWebsiteCommandHandler
        : IRequestHandler<AddWebsiteCommand, IReadOnlyCollection<Website>>
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly CheckWebsiteAvailabilityClient _checkWebsiteAvailabilityClient;

        public AddWebsiteCommandHandler(IWebsiteRepository websiteRepository, CheckWebsiteAvailabilityClient checkWebsiteAvailabilityClient)
        {
            _websiteRepository = websiteRepository;
            _checkWebsiteAvailabilityClient = checkWebsiteAvailabilityClient;
        }

        public async Task<IReadOnlyCollection<Website>> Handle(AddWebsiteCommand command, CancellationToken cancellationToken)
        {
            var tasks = command.Addresses
                .Select(a => HandleUrlAddress(a, cancellationToken))
                .ToList();
            
            var websites = await Task.WhenAll(tasks);
            return websites.Adapt<IReadOnlyCollection<Website>>();
        }

        private async Task<DbWebsite> HandleUrlAddress(string address, CancellationToken cancellationToken)
        {
            var availability = await _checkWebsiteAvailabilityClient.CheckAsync(address);
            var website = new DbWebsite
            {
                Address = address, 
                StatusCode = availability.StatusCode, 
                Available = availability.Available
            };

            return await _websiteRepository.CreateAsync(website, cancellationToken);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class WebsiteAddCommandHandler 
        : IRequestHandler<WebsiteAddCommand, IEnumerable<Website>>
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly WebsiteCheckerClient _websiteCheckerClient;

        public WebsiteAddCommandHandler(IWebsiteRepository websiteRepository, WebsiteCheckerClient websiteCheckerClient)
        {
            _websiteRepository = websiteRepository;
            _websiteCheckerClient = websiteCheckerClient;
        }

        public async Task<IEnumerable<Website>> Handle(WebsiteAddCommand command, CancellationToken cancellationToken)
        {
            var tasks = command.Addresses.Select(HandleUrlAddress);
            var websites = await Task.WhenAll(tasks);
            
            var result = websites.Select(x => new Website
            {
                Id = x.Id,
                Address = x.Address,
                Status = x.StatusAsString
            });

            return result;
        }

        private async Task<DbWebsite> HandleUrlAddress(string address)
        {
            var isAccessed = await _websiteCheckerClient.CheckAsync(address);
            var website = new DbWebsite {Address = address, Status = isAccessed};

            return await _websiteRepository.CreateAsync(website);
        }
    }
}
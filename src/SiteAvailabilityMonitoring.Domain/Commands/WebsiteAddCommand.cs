using System.Collections.Generic;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class WebsiteAddCommand
        : IRequest<IEnumerable<Website>>
    {
        public WebsiteAddCommand(IReadOnlyCollection<string> addresses)
        {
            Addresses = addresses;
        }

        public IReadOnlyCollection<string> Addresses { get; }
    }
}
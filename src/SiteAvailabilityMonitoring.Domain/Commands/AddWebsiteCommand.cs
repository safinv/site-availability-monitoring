using System.Collections.Generic;
using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class AddWebsiteCommand
        : IRequest<IReadOnlyCollection<Website>>
    {
        public AddWebsiteCommand(IReadOnlyCollection<string> addresses)
        {
            Addresses = addresses;
        }

        public IReadOnlyCollection<string> Addresses { get; }
    }
}
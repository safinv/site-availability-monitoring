using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class AddWebsiteCommand
        : IRequest<Website>
    {
        public string Address { get; }

        public AddWebsiteCommand(string address)
        {
            Address = address;
        }
    }
}
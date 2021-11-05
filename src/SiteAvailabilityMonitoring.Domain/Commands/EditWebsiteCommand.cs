using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class EditWebsiteCommand
        : IRequest<Website>
    {
        public long Id { get; }
        public string Address { get; }

        public EditWebsiteCommand(long id, string address)
        {
            Id = id;
            Address = address;
        }
    }
}
using MediatR;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class DeleteWebsiteCommand
        : IRequest
    {
        public DeleteWebsiteCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
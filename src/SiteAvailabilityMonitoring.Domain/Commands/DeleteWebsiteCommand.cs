using MediatR;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class DeleteWebsiteCommand
        : IRequest
    {
        public long Id { get; }

        public DeleteWebsiteCommand(long id)
        {
            Id = id;
        }
    }
}
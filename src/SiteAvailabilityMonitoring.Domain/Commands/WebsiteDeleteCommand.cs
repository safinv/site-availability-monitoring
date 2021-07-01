using MediatR;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class WebsiteDeleteCommand
        : IRequest
    {
        public WebsiteDeleteCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
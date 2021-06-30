using MediatR;

namespace SiteAvailabilityMonitoring.Domain.Commands
{
    public class WebsiteEditCommand : IRequest<Unit>
    {
        public WebsiteEditCommand(long id, string address)
        {
            Id = id;
            Address = address;
        }

        public long Id { get; }
        public string Address { get; }
    }
}
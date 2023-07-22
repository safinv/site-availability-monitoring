using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Commands;

public class EditWebsiteCommand
    : IRequest<Website>
{
    public EditWebsiteCommand(long id, string address)
    {
        Id = id;
        Address = address;
    }

    public long Id { get; }
    public string Address { get; }
}
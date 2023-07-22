using MediatR;
using SiteAvailabilityMonitoring.Abstractions.Dto;

namespace SiteAvailabilityMonitoring.Domain.Commands;

public class AddWebsiteCommand
    : IRequest<Website>
{
    public AddWebsiteCommand(string address)
    {
        Address = address;
    }

    public string Address { get; }
}
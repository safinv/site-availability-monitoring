using System;

namespace SiteAvailabilityMonitoring.Entities
{
    public class Website
    {
        public Website(long id, string address, string status)
        {
            Id = id;
            Address = address;
            Status = Enum.Parse<Status>(status);
        }

        public Website(string address, Status status)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Status = status;
        }

        public long Id { get; private set; }
        public string Address { get; private set;}
        public Status Status { get; private set;}

        public string StatusAsString => Status.ToString().ToLower();

        public void SetStatus(Status status)
        {
            Status = status;
        }
        
        public void SetAddress(string address)
        {
            Address = address;
        }
    }
}
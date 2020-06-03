using System;

namespace SiteAvailabilityMonitoring.Entities.Models
{
    public class WebsiteModel
    {
        public WebsiteModel(string address, bool status = default)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Status = status;
        }

        public string Address { get; }
        
        public bool Status { get; }
    }
}
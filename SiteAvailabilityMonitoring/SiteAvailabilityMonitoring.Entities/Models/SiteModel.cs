using System;

namespace SiteAvailabilityMonitoring.Entities.Models
{
    public class SiteModel
    {
        public SiteModel(string name, bool status = default)
        {
            Address = name ?? throw new ArgumentNullException();
            Status = status;
        }

        public string Address { get; }
        
        public bool Status { get; }
    }
}
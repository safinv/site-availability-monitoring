using System.Collections.Generic;

using SiteAvailabilityMonitoring.Domain.Models;

namespace SiteAvailabilityMonitoring.Models
{
    public class AdministrationPageModel
    {
        public AdministrationPageModel(IEnumerable<Site> sites, Background applicationSettingModel)
        {
            Sites = sites;
            Time = string.Join(":", applicationSettingModel.Hour, applicationSettingModel.Minutes, applicationSettingModel.Seconds);
        }

        public IEnumerable<Site> Sites { get; }

        public string Time { get; }
    }
}
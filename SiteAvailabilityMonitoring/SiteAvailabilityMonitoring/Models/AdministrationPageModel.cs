using Domain.Models;
using System.Collections.Generic;

namespace SiteAvailabilityMonitoring.Models
{
    public class AdministrationPageModel
    {
        public AdministrationPageModel(IList<UrlModel> urlModels, BackgroundSettingModel applicationSettingModel)
        {
            UrlModels = urlModels;
            Time = string.Join(":", applicationSettingModel.Hour, applicationSettingModel.Minutes, applicationSettingModel.Seconds);
        }

        public IList<UrlModel> UrlModels { get; } 

        public string Time { get; }
    }
}

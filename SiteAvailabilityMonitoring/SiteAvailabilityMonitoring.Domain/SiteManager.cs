using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities.DboModels;
using SiteAvailabilityMonitoring.Entities.Models;

namespace SiteAvailabilityMonitoring.Domain
{
    public class SiteManager : ISiteManager
    {
        private readonly ISiteRepository _siteRepository;
        private readonly SiteCheckerClient _siteCheckerClient;
        
        public SiteManager(ISiteRepository siteRepository, SiteCheckerClient siteCheckerClient)
        {
            _siteRepository = siteRepository;
            _siteCheckerClient = siteCheckerClient;
        }

        public async Task<IEnumerable<SiteModel>> GetAllAsync()
        {
            var sites = await _siteRepository.GetAllAsync();
            return sites.Select(elem => new SiteModel(elem.Address, elem.Status));
        }

        public async Task CreateAsync(SiteModel siteModel)
        {
            var site = new Site
            {
                Address = siteModel.Address
            };

            await _siteRepository.CreateAsync(site);
        }

        public async Task CheckOnAccessAndUpdate()
        {
            var sites = await _siteRepository.GetAllAsync();

            foreach (var site in sites)
            {
                var isAccessed = await _siteCheckerClient.CheckAsync(site.Address);
                if (isAccessed != site.Status)
                {
                    site.Status = isAccessed;
                    await _siteRepository.UpdateAsync(site);
                }
            }
        }
    }
}
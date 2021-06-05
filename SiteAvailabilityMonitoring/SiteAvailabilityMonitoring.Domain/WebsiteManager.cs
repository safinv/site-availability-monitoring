using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.Contracts;
using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities;

namespace SiteAvailabilityMonitoring.Domain
{
    public class WebsiteManager : IWebsiteManager
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly WebsiteCheckerClient _websiteCheckerClient;
        
        public WebsiteManager(IWebsiteRepository websiteRepository, WebsiteCheckerClient websiteCheckerClient)
        {
            _websiteRepository = websiteRepository;
            _websiteCheckerClient = websiteCheckerClient;
        }

        public async Task<IEnumerable<Website>> GetAllAsync()
        {
            var sites = await _websiteRepository.GetAllAsync();
            return sites;
        }

        public async Task CreateAsync(List<string> addresses)
        {
            foreach (var address in addresses)
            {
                var cleanAddress = address.TrimEnd('/');
                var status = await _websiteCheckerClient.CheckAsync(cleanAddress);
                var website = new Website(address, status);

                await _websiteRepository.CreateAsync(website);
            }
        }

        public async Task EditAsync(long id, string address)
        {
            var status = await _websiteCheckerClient.CheckAsync(address);
            var website = new Website(id, address, status);

            await _websiteRepository.UpdateAsync(website);
        }

        public async Task CheckAllOnAccessAndUpdate()
        {
            var websites = await _websiteRepository.GetAllAsync();
            var tasks = websites.Select(UpdateStatus).ToList();

            await Task.WhenAll(tasks);
        }

        private async Task UpdateStatus(Website website)
        {
            var isAccessed = await _websiteCheckerClient.CheckAsync(website.Address);
            if (isAccessed != website.Status)
            {
                website.SetStatus(isAccessed);
                await _websiteRepository.UpdateAsync(website);
            }
        }
    }
}
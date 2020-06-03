using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Domain.DataAccessPoint;
using SiteAvailabilityMonitoring.Entities.DbModels;
using SiteAvailabilityMonitoring.Entities.Models;

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

        public async Task<IEnumerable<WebsiteModel>> GetAllAsync()
        {
            var sites = await _websiteRepository.GetAllAsync();
            return sites.Select(elem => new WebsiteModel(elem.Address, elem.Status));
        }

        public async Task CreateAsync(WebsiteModel websiteModel)
        {
            var status = await _websiteCheckerClient.CheckAsync(websiteModel.Address);
            var website = new Website
            {
                Address = websiteModel.Address,
                Status = status
            };

            await _websiteRepository.CreateAsync(website);
        }

        public async Task CheckOnAccessAndUpdate()
        {
            var websites = await _websiteRepository.GetAllAsync();
            var tasks = websites.Select(website => Task.Run(async () =>
                {
                    var isAccessed = await _websiteCheckerClient.CheckAsync(website.Address);
                    if (isAccessed != website.Status)
                    {
                        website.Status = isAccessed;
                        await _websiteRepository.UpdateAsync(website);
                    }
                }))
                .ToList();

            var continuation = Task.WhenAll(tasks);
            try {
                continuation.Wait();
            }
            catch (AggregateException)
            { }
        }
    }
}
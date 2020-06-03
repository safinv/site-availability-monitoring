using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain;
using SiteAvailabilityMonitoring.Dto;
using SiteAvailabilityMonitoring.Entities.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    [Route("sites")]
    public class WebsiteManagerController : Controller
    {
        private readonly IWebsiteManager _websiteManager;
        
        public WebsiteManagerController(IWebsiteManager websiteManager)
        {
            _websiteManager = websiteManager;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<WebsiteObjectGetDto>> GetAll()
        {
            var sites = await _websiteManager.GetAllAsync();
            return sites.Select(site => new WebsiteObjectGetDto
            {
                Address = site.Address,
                Status = site.Status ? "nice" : "bad"
            });
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> AddWebsite([FromBody] WebsiteObjectCreateDto dto)
        {
            await _websiteManager.CreateAsync(new WebsiteModel(dto.Address));
            return Ok();
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            await _websiteManager.CheckOnAccessAndUpdate();
            return Ok();
        }
    }
}
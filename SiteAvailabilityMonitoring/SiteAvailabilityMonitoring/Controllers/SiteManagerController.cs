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
    public class SiteManagerController : Controller
    {
        private readonly ISiteManager _siteManager;
        
        public SiteManagerController(ISiteManager siteManager)
        {
            _siteManager = siteManager;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<SiteObjectGetDto>> GetAll()
        {
            var sites = await _siteManager.GetAllAsync();
            return sites.Select(site => new SiteObjectGetDto
            {
                Address = site.Address,
                Status = site.Status ? "nice" : "bad"
            });
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> AddSite([FromBody] SiteObjectCreateDto dto)
        {
            await _siteManager.CreateAsync(new SiteModel(dto.Address));
            return Ok();
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            await _siteManager.CheckOnAccessAndUpdate();
            return Ok();
        }
    }
}
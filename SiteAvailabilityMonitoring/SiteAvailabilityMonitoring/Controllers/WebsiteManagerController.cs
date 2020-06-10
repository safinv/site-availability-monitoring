using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain.Contracts;
using SiteAvailabilityMonitoring.Dto;

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
            var websites = await _websiteManager.GetAllAsync();
            return websites.Select(website => new WebsiteObjectGetDto
            {
                Id = website.Id,
                Address = website.Address,
                Status = website.StatusAsString
            });
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> AddWebsite([FromBody] WebsiteObjectCreateDto dto)
        {
            await _websiteManager.CreateAsync(dto.Address);
            return Ok();
        }
        
        [HttpPut("edit")]
        public async Task<IActionResult> EditWebsite([FromBody] WebsiteObjectEditDto dto)
        {
            await _websiteManager.EditAsync(dto.Id, dto.Address);
            return Ok();
        }

        [HttpPost("check")]
        public async Task<IActionResult> Check()
        {
            await _websiteManager.CheckOnAccessAndUpdate();
            return Ok();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.Commands;
using SiteAvailabilityMonitoring.Domain.Queries;

namespace SiteAvailabilityMonitoring.Api.Controllers
{
    [ApiController]
    [Route("api/website")]
    public class WebsiteManagerController : Controller
    {
        private readonly IMediator _mediator;

        public WebsiteManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get all websites.
        /// </summary>
        /// <returns><see cref="Website"/></returns>
        [HttpGet]
        public async Task<IEnumerable<Website>> Get()
        {
            var command = new GetWebsitesQuery();
            var result = await _mediator.Send(command);

            return result;
        }
        
        /// <summary>
        ///     Get website.
        /// </summary>
        /// <returns><see cref="Website"/></returns>
        [HttpGet("{id:long}")]
        public async Task<Website> Get(long id)
        {
            var command = new GetWebsiteQuery(id);
            var result = await _mediator.Send(command);

            return result;
        }

        /// <summary>
        ///     Added website urls.
        /// </summary>
        /// <param name="model"><see cref="WebsiteAdd"/></param>
        /// <returns>Id's</returns>
        [HttpPost]
        public async Task<IEnumerable<Website>> Add([FromBody] WebsiteAdd model)
        {
            var command = new AddWebsiteCommand(model.Addresses);
            var result = await _mediator.Send(command);

            return result;
        }

        /// <summary>
        ///     Change url.
        /// </summary>
        /// <param name="model"><see cref="WebsiteEdit"/></param>
        [HttpPut]
        public async Task<Website> Edit([FromBody] WebsiteEdit model)
        {
            var command = new EditWebsiteCommand(model.Id, model.Address);
            var result = await _mediator.Send(command);

            return result;
        }

        /// <summary>
        ///     Delete row.
        /// </summary>
        /// <param name="id">Identity website.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteWebsiteCommand(id);
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        ///     Availability urls check.
        /// </summary>
        [HttpPost("check")]
        public async Task<IActionResult> Check()
        {
            var command = new CheckAvailabilityCommand();
            await _mediator.Send(command);

            return Ok();
        }
    }
}
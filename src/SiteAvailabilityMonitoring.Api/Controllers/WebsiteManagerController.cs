using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteAvailabilityMonitoring.Abstractions.Dto;
using SiteAvailabilityMonitoring.Domain.Commands;
using SiteAvailabilityMonitoring.Domain.Queries;

namespace SiteAvailabilityMonitoring.Api.Controllers
{
    [ApiController]
    [Route("api/website")]
    public class WebsiteManagerController
        : Controller
    {
        private readonly IMediator _mediator;

        public WebsiteManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get websites.
        /// </summary>
        /// <returns><see cref="Website"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<Website>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int limit = 10, int offset = 0)
        {
            var command = new GetWebsitesQuery(limit, offset);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        ///     Get website.
        /// </summary>
        /// <returns><see cref="Website"/></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(Website), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(long id)
        {
            var command = new GetWebsiteQuery(id);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        ///     Added website urls.
        /// </summary>
        /// <param name="model"><see cref="WebsiteAdd"/></param>
        /// <returns><see cref="Website"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Website), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] WebsiteAdd model)
        {
            var command = new AddWebsiteCommand(model.Address);
            var result = await _mediator.Send(command);

            if (result == null) return BadRequest($"Address '{model.Address}' already exsist.");

            return Created("test", result);
        }

        /// <summary>
        ///     Change url.
        /// </summary>
        /// <param name="model"><see cref="WebsiteEdit"/></param>
        [HttpPut]
        [ProducesResponseType(typeof(Website), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromBody] WebsiteEdit model)
        {
            var command = new EditWebsiteCommand(model.Id, model.Address);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        ///     Delete url.
        /// </summary>
        /// <param name="id">Identity website.</param>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteWebsiteCommand(id);
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        ///     Availability all urls check.
        /// </summary>
        [HttpPost("check")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Check()
        {
            var command = new CheckAvailabilityCommand();
            await _mediator.Send(command);

            return Ok();
        }
    }
}
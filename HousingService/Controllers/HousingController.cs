using MediatR;
using Microsoft.AspNetCore.Mvc;
using HousingService.Housings.Commands;
using HousingService.Housings.Models;
using HousingService.Housings.Queries;
using HousingService.Offers.Queries;

namespace HousingService.Api.Controllers
{
    public class HousingController : ApiController
    {
        private readonly IMediator _mediator;

        public HousingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHousingById(int id)
        {
            var result = await Mediator.Send(new GetHousingsByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<HousingDTO>>> GetHousings([FromQuery] GetHousingsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddHousing([FromBody] AddHousingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteHousing([FromBody] DeleteHousingCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHousing(int id, [FromBody] UpdateHousingsCommand command)
        {
            command.HousingId = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}/offers")]
        public async Task<IActionResult> GetOffersByHousingId(int id)
        {
            var result = await Mediator.Send(new GetOffersByHousingIdQuery(id));
            return Ok(result);
        }
    }
}
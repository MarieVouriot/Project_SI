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
        [HttpGet]
        [Route("getHousingById")]
        public async Task<ActionResult<HousingDTO>> GetHousingById([FromQuery] GetHousingByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("getHousings")]
        public async Task<ActionResult<List<HousingDTO>>> GetHousings([FromQuery] GetHousingsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("addHousing")]
        public async Task<ActionResult> AddHousing([FromBody] AddHousingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteHousing([FromBody] DeleteHousingCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpPut]
        [Route("updateHousing")]
        public async Task<ActionResult<Unit>> UpdateHousing([FromBody] UpdateHousingCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}/offers")]
        public async Task<ActionResult> GetOffersByHousingId(int id)
        {
            var result = await Mediator.Send(new GetOffersOfHousingQuery(id));
            return Ok(result);
        }
    }
}
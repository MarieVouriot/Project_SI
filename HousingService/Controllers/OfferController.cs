using HousingService.Api.Controllers;
using HousingService.Housings.Commands;
using HousingService.Offers.Commands;
using HousingService.Offers.Models;
using HousingService.Offers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HousingService.Controllers
{
    public class OfferController : ApiController
    {
        [HttpGet]
        [Route("getOffers")]
        public async Task<ActionResult<List<OfferDTO>>> GetOffers([FromQuery] GetOffersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("addOffer")]
        public async Task<ActionResult<Unit>> AddOffer([FromBody] AddOfferCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpPut]
        [Route("updateOffer")]
        public async Task<ActionResult<Unit>> UpdateOffer([FromBody] UpdateOfferCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        [Route("deleteOffer")]
        public async Task<ActionResult<Unit>> DeleteOffer([FromBody] DeleteOfferCommand cmd)
        {
            return await Mediator.Send(cmd);
        }
    }
}

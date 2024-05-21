﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Housings.Commands;
using ReservationService.Housings.Models;
using ReservationService.Housings.Queries;

namespace ReservationService.Api.Controllers
{
    public class HousingController : ApiController
    {
        [HttpGet]
        [Route("getHousings")]
        public async Task<ActionResult<List<HousingDTO>>> GetHousings([FromQuery] GetHousingsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("addHousing")]
        public async Task<ActionResult<Unit>> AddHousing([FromBody] AddHousingCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpPost]
        [Route("deleteHousing")]
        public async Task<ActionResult<Unit>> DeleteHousing([FromBody] DeleteHousingCommand cmd)
        {
            return await Mediator.Send(cmd);
        }
    }
}
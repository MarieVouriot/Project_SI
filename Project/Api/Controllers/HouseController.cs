using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Users.Commands;
using Project.Application.Users.Models;
using Project.Application.Users.Queries;

namespace Project.Api.Controllers
{
    public class HouseController : ApiController
    {
        [HttpGet]
        [Route("getHouses")]
        public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
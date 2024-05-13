using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Users.Commands;
using Project.Users.Models;
using Project.Users.Queries;

namespace Project.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("addUser")]  
        public async Task<ActionResult<Unit>> AddUser([FromBody] AddUserCommand cmd)
        {
            return await Mediator.Send(cmd);
        }
    }
}

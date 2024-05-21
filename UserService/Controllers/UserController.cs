using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Users.Commands;
using UserService.Users.Models;
using UserService.Users.Queries;

namespace UserService.Api.Controllers
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

        [HttpPost]
        [Route("deleteUser")]
        public async Task<ActionResult<Unit>> deleteUser([FromBody] DeleteUserCommand cmd)
        {
            return await Mediator.Send(cmd);
        }
    }
}

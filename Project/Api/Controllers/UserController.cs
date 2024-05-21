using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Users.Commands;
using Project.Application.Users.Models;
using Project.Application.Users.Queries;

namespace Project.Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] GetUserQuery query)
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
        public async Task<ActionResult<Unit>> DeleteUser([FromBody] DeleteUserCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpPost]
        [Route("updateUser")]
        public async Task<ActionResult<Unit>> UpdateUser([FromBody] UpdateUserCommand cmd)
        {
            return await Mediator.Send(cmd);
        }
    }
}

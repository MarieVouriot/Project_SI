using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Users.Commands;
using UserService.Users.Models;
using UserService.Users.Queries;
using UserService.Housings.Models;

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


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteUser([FromBody] DeleteUserCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<ActionResult<Unit>> UpdateUser([FromBody] UpdateUserCommand cmd)
        {
            return await Mediator.Send(cmd);
        }

        [HttpGet]
        [Route("user/{userId}/housings")]
        public async Task<ActionResult<List<HousingDTO>>> GetUserHousings(int userId)
        {
            var query = new GetUserHousingsQuery { UserId = userId };
            return await Mediator.Send(query);
        }
    }
}

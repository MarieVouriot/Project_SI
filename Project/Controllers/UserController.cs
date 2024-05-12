using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Users.Commands;
using Project.Users.Models;
using Project.Users.Queries;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<ActionResult<Unit>> AssUser([FromBody] AddUserCommand cmd)
        {
            return await _mediator.Send(cmd);
        }
    }
}

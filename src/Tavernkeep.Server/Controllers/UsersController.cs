using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Users.Commands.CreateUser;
using Tavernkeep.Core.Contracts.Users;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(ILogger<UsersController> logger, IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="request">Request with user's parameters.</param>
        /// <returns>Created user.</returns>
        [HttpPost("create", Name = "Create User")]
        public async Task<User> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await mediator.Send(new CreateUserCommand(request.Login, request.Password));
            return user;
        }
    }
}

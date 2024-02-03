using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Users.Commands.CreateUser;
using Tavernkeep.Core.Contracts.Users;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    /// Controller for managing user operations.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">Request with user's parameters.</param>
        /// <returns>Created user.</returns>
        [HttpPost("create")]
        public async Task<User> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await mediator.Send(new CreateUserCommand(request.Login, request.Password, request.Role));
            return user;
        }
    }
}

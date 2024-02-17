using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Users.Commands.CreateUser;
using Tavernkeep.Application.Actions.Users.Commands.DeleteUser;
using Tavernkeep.Application.Actions.Users.Queries.GetUsers;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Users;
using Tavernkeep.Core.Entities;
using Tavernkeep.Server.Middleware;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    /// The <see cref="UsersController"/> class handles user operations within the application.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of all registered users.</returns>
        [Authorize]
        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            var users = await mediator.Send(new GetAllUsersQuery());
            return users;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">Request with user's parameters.</param>
        /// <returns>Created user.</returns>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpPost("create")]
        public async Task<User> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await mediator.Send(new CreateUserCommand(request.Login, request.Password, request.Role));
            return user;
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="userId">The user ID for deletion.</param>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpDelete("delete/{userId}")]
        public async Task DeleteUser([FromRoute] Guid userId)
        {
            await mediator.Send(new DeleteUserCommand(userId));
        }
    }
}

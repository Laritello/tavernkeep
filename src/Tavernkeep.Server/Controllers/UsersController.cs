using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Users.Commands.CreateUser;
using Tavernkeep.Application.Actions.Users.Commands.DeleteUser;
using Tavernkeep.Application.Actions.Users.Queries.GetUsers;
using Tavernkeep.Application.UseCases.Users.Commands.EditUser;
using Tavernkeep.Application.UseCases.Users.Queries.GetCurrentUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Users.Requests;
using Tavernkeep.Core.Entities;
using Tavernkeep.Server.Extensions;
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
        /// <param name="request">Request with user's properties.</param>
        /// <returns>Created user.</returns>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpPost]
        public async Task<User> CreateUser([FromBody] CreateUserRequest request)
        {
            return await mediator.Send(new CreateUserCommand(request.Login, request.Password, request.Role));
        }

        /// <summary>
        /// Edit an existing user.
        /// </summary>
        /// <param name="userId">The edited user ID.</param>
        /// <param name="request">Request with updated properties.</param>
        /// <returns></returns>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpPatch("{userId}")]
        public async Task<User> EditUser([FromRoute] Guid userId, [FromBody] EditUserRequest request)
        {
            return await mediator.Send(new EditUserCommand(userId, request.Login, request.Password, request.Role));
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="userId">The user ID for deletion.</param>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpDelete("{userId}")]
        public async Task DeleteUser([FromRoute] Guid userId)
        {
            await mediator.Send(new DeleteUserCommand(userId));
        }

        /// <summary>
        /// Get an authorized user.
        /// </summary>
        /// <returns>The user that was specified in the authorization token.</returns>
        [Authorize]
        [HttpGet("current")]
        public async Task<User> GetCurrentUser()
        {
            return await mediator.Send(new GetUserQuery(HttpContext.GetUserId()));
        }
    }
}

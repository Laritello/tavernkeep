using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken;
using Tavernkeep.Core.Contracts.Authentication.Requests;
using Tavernkeep.Core.Contracts.Authentication.Responses;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    /// The <see cref="AuthenticationController"/> class handles user authentication and authorization within the application.
    /// </summary>
    /// <param name="mediator"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Generate authentication JWT for the user.
        /// </summary>
        /// <param name="request">The authentication request.</param>
        /// <returns>The authentication JWT.</returns>
        [HttpPost("auth")]
        public async Task<AuthenticationResponse> CreateAuthenticationToken(AuthenticationRequest request)
        {
            return await mediator.Send(new CreateAuthenticationTokenCommand(request.Login, request.Password));
        }
    }
}

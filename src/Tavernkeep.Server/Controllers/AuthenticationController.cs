using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken;
using Tavernkeep.Application.UseCases.Authentication.Commands.RefreshAuthenticationToken;
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
        /// Generate authentication tokens for the user.
        /// </summary>
        /// <param name="request">The authentication request.</param>
        /// <returns>The authentication JWT and refresh token.</returns>
        [HttpPost("auth")]
        public async Task<AuthenticationResponse> CreateAuthenticationToken(AuthenticationRequest request)
        {
            return await mediator.Send(new CreateAuthenticationTokenCommand(request.Login, request.Password));
        }

        /// <summary>
        /// Refresh existing tokens for the user.
        /// </summary>
        /// <param name="request">The refresh token request.</param>
        /// <returns>The authentication JWT and refresh token.</returns>
        [HttpPost("refresh")]
        public async Task<AuthenticationResponse> RefreshToken(RefreshTokenRequest request)
        {
            return await mediator.Send(new RefreshAuthenticationTokenCommand(request.AccessToken, request.RefreshToken));
        }
    }
}

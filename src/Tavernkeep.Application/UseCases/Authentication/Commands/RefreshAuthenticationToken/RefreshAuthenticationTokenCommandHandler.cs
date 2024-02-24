using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Authentication.Responses;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Authentication.Commands.RefreshAuthenticationToken
{
    public class RefreshAuthenticationTokenCommandHandler(IUserRepository userRepository, IAuthTokenService tokenService) : IRequestHandler<RefreshAuthenticationTokenCommand, AuthenticationResponse>
    {
        public async Task<AuthenticationResponse> Handle(RefreshAuthenticationTokenCommand request, CancellationToken cancellationToken)
        {
            //string accessToken = request.AccessToken;
            //string refreshToken = request.RefreshToken;

            //var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);

            //var username = principal.Identity.Name; //this is mapped to the Name claim by default
            //var user = await userRepository.GetUserByLoginAsync(username);

            //if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            //    return BadRequest("Invalid client request");
            //var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            //var newRefreshToken = _tokenService.GenerateRefreshToken();
            //user.RefreshToken = newRefreshToken;
            //_userContext.SaveChanges();
            //return Ok(new AuthenticatedResponse()
            //{
            //    Token = newAccessToken,
            //    RefreshToken = newRefreshToken
            //});

            return new AuthenticationResponse()
            {
                AccessToken = request.AccessToken,
                RefreshToken = request.RefreshToken,
            };
        }
    }
}

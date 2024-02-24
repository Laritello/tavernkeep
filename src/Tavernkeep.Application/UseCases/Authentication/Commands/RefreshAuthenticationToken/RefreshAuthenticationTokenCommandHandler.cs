using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Contracts.Authentication.Responses;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Authentication.Commands.RefreshAuthenticationToken
{
    public class RefreshAuthenticationTokenCommandHandler(IUserRepository userRepository, IRefreshTokenRepository tokenRepository, IAuthTokenService tokenService) : IRequestHandler<RefreshAuthenticationTokenCommand, AuthenticationResponse>
    {
        public async Task<AuthenticationResponse> Handle(RefreshAuthenticationTokenCommand request, CancellationToken cancellationToken)
        {
            string accessToken = request.AccessToken;
            string refreshToken = request.RefreshToken;

            var principal = await tokenService.GetUserIdentityFromExpiredToken(accessToken);
            var id = principal.FindFirst(JwtCustomClaimNames.UserId)!.Value;

            var user = await userRepository.FindAsync(new Guid(id))
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var tokens = await tokenRepository.GetTokensForUser(user.Id, cancellationToken);

            if (!tokens.Any(x => x.Token == refreshToken))
                throw new BusinessLogicException("Specified refresh token isn't registered.");

            var token = tokens.First(x => x.Token == refreshToken);

            if (DateTime.UtcNow.CompareTo(token.Expires) > 0)
                throw new BusinessLogicException("Expired refresh token provided.");

            var newAccessToken = tokenService.GenerateAccessToken(user);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            tokenRepository.Save(new Core.Entities.RefreshToken()
            {
                UserId = user.Id,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            await tokenRepository.CommitAsync(cancellationToken);

            return new AuthenticationResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}

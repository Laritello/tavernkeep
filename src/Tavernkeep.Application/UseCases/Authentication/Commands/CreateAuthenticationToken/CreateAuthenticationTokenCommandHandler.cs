using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Authentication.Responses;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken
{
    public class CreateAuthenticationTokenCommandHandler(IUserRepository repository, IRefreshTokenRepository tokenRepository, IAuthTokenService tokenService) : IRequestHandler<CreateAuthenticationTokenCommand, AuthenticationResponse>
    {
        public async Task<AuthenticationResponse> Handle(CreateAuthenticationTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Login))
                throw new BusinessLogicException("No user login provided..");

            var user = await repository.GetUserByLoginAsync(request.Login, cancellationToken) 
                ?? throw new BusinessLogicException("No user with provided login exists.");

            if (user.Password != request.Password)
                throw new BusinessLogicException("Passwords do not match.");

            var token = tokenService.GenerateAccessToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();

            tokenRepository.Save(new RefreshToken()
            {
                UserId = user.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            await tokenRepository.CommitAsync(cancellationToken);

            return new AuthenticationResponse() 
            {
                AccessToken = token,
                RefreshToken = refreshToken,
            };
        }
    }
}

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Authentication.Commands.CreateAuthenticationnToken
{
    public class CreateAuthenticationTokenCommandHandler(IUserRepository repository, IConfiguration configuration) : IRequestHandler<CreateAuthenticationTokenCommand, string>
    {
        public async Task<string> Handle(CreateAuthenticationTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Login))
                throw new BusinessLogicException("No user login provided..");

            var user = await repository.GetUserByLoginAsync(request.Login, cancellationToken) 
                ?? throw new BusinessLogicException("No user with provided login exists.");

            if (user.Password != request.Password)
                throw new BusinessLogicException("Passwords do not match.");

            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? string.Empty);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", Guid.NewGuid().ToString()),
                    new Claim(JwtCustomClaimNames.UserId, user.Id.ToString()),
                    new Claim(JwtCustomClaimNames.UserLogin, user.Login),
                    new Claim(JwtCustomClaimNames.UserRole, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}

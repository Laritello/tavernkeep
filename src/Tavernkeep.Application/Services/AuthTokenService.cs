using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Services
{
	public class AuthTokenService(IConfiguration configuration) : IAuthTokenService
	{
		public string GenerateAccessToken(User user)
		{
			var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? string.Empty);
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(
				[
					new Claim("id", Guid.NewGuid().ToString()),
					new Claim(JwtCustomClaimNames.UserId, user.Id.ToString()),
					new Claim(JwtCustomClaimNames.UserLogin, user.Login),
					new Claim(JwtCustomClaimNames.UserRole, user.Role.ToString()),
				]),
				Expires = DateTime.UtcNow.AddMinutes(5),
				SigningCredentials = signingCredentials
			};

			var token = new JsonWebTokenHandler().CreateToken(tokenDescriptor);
			return token;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();

			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		public async Task<ClaimsIdentity> GetUserIdentityFromExpiredTokenAsync(string token)
		{
			var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? string.Empty);

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateLifetime = false
			};

			var tokenHandler = new JsonWebTokenHandler();
			var validationResult = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

			if (!validationResult.IsValid)
				throw new SecurityTokenException("Invalid token");

			return validationResult.ClaimsIdentity;
		}
	}
}

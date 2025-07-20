using System.Security.Claims;
using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Application.Interfaces
{
	public interface IAuthTokenService
	{
		string GenerateAccessToken(User user);
		string GenerateRefreshToken();
		Task<ClaimsIdentity> GetUserIdentityFromExpiredTokenAsync(string token);
	}
}

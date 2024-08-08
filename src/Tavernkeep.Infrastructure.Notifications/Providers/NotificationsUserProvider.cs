using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Authentication;

namespace Tavernkeep.Infrastructure.Notifications.Providers
{
	/// <summary>
	/// Provides user id for the specified connection from the <see cref="HubConnectionContext"/>.
	/// </summary>
	public class NotificationsUserProvider : IUserIdProvider
	{
		public string? GetUserId(HubConnectionContext connection)
		{
			if (connection.GetHttpContext() == null)
				return null;

			return connection.GetHttpContext()!.User.FindFirst(JwtCustomClaimNames.UserId)?.Value;
		}
	}
}

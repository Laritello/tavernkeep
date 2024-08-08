using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Infrastructure.Notifications.Storage;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
	public class BaseHub<T>([FromServices] IUserConnectionStorage<Guid> userStorage) : Hub<T> where T : class
	{
		public override Task OnConnectedAsync()
		{
			userStorage.Add(GetUserId(), Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			userStorage.Remove(GetUserId(), Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}


		private Guid GetUserId()
		{
			var user = Context.User ?? throw new NotAuthorizedException("Access denied. Please provide valid credentials.");
			var claim = user.FindFirst(JwtCustomClaimNames.UserId) ?? throw new NotAuthorizedException("Access denied. Please provide valid credentials.");

			return Guid.Parse(claim.Value);
		}
	}
}

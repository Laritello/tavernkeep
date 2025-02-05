using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Character.Dtos;

namespace Tavernkeep.Infrastructure.Notifications.Hubs
{
	public interface ICharacterHub
	{
		Task OnCharacterCreated(CharacterDto character);
		Task OnCharacterEdited(CharacterDto character);
		Task OnCharacterDeleted(Guid characterId);
	}

	public class CharacterHub : Hub<ICharacterHub> { }
}

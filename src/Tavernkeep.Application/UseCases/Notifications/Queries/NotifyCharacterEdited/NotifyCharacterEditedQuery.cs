using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Notifications.Queries.NotifyCharacterEdited
{
	public class NotifyCharacterEditedQuery(Character character) : IRequest
	{
		public Character Character { get; set; } = character;
	}
}

using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
	public class CreateCharacterCommand(Guid ownerId, string name, string ancestryId, string backgroundId, string classId) : IRequest<Character>
	{
		public Guid OwnerId { get; set; } = ownerId;
		public string Name { get; set; } = name;
		public string AncestryId { get; set; } = ancestryId;
		public string BackgroundId { get; set; } = backgroundId;
		public string ClassId { get; set; } = classId;
	}
}

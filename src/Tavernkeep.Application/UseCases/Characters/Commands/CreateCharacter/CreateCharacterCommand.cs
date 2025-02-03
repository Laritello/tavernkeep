using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
	public class CreateCharacterCommand(Guid ownerId, CharacterTemplateDto character) : IRequest<Character>
	{
		public Guid OwnerId { get; set; } = ownerId;
		public CharacterTemplateDto Character { get; set; } = character;
	}
}

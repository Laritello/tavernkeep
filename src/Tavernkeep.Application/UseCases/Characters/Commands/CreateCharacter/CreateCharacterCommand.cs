using MediatR;
using Tavernkeep.Domain.Contracts.Character.Dtos;
using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
	public class CreateCharacterCommand(Guid ownerId, CharacterTemplateDto character) : IRequest<Character>
	{
		public Guid OwnerId { get; set; } = ownerId;
		public CharacterTemplateDto Character { get; set; } = character;
	}
}

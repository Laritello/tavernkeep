using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
	public class CreateCharacterCommand(Guid ownerId, string name) : IRequest<Character>
	{
		public Guid OwnerId { get; set; } = ownerId;
		public string Name { get; set; } = name;
	}
}

using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest<CharacterDto>
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }

        public CreateCharacterCommand(Guid ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
        }
    }
}

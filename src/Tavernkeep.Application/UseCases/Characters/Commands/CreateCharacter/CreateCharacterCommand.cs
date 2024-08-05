using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest<Character>
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

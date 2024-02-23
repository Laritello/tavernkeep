using MediatR;

namespace Tavernkeep.Application.Actions.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommand : IRequest
    {
        public Guid CharacterId { get; set; }

        public DeleteCharacterCommand(Guid characterId)
        {
            CharacterId = characterId;
        }
    }
}

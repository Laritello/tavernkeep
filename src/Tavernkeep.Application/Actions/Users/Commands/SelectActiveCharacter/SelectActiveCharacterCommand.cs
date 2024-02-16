using MediatR;

namespace Tavernkeep.Application.Actions.Users.Commands.SelectActiveCharacter
{
    public class SelectActiveCharacterCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid CharacterId { get; set; }

        public SelectActiveCharacterCommand(Guid userId, Guid characterId)
        {
            UserId = userId;
            CharacterId = characterId;
        }
    }
}

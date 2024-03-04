using MediatR;
using Tavernkeep.Core.Contracts.Users.Dtos;

namespace Tavernkeep.Application.UseCases.Users.Commands.SetActiveCharacter
{
    public class SetActiveCharacterCommand : IRequest<UserDto>
    {
        public Guid InitiatorId { get; set; }
        public Guid UserId { get; set; }
        public Guid CharacterId { get; set; }

        public SetActiveCharacterCommand(Guid initiatorId, Guid userId, Guid characterId)
        {
            InitiatorId = initiatorId;
            UserId = userId;
            CharacterId = characterId;
        }
    }
}

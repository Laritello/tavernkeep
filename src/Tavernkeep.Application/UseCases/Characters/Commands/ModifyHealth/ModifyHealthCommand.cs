using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Characters.Commands.ModifyHealth
{
    public class ModifyHealthCommand : IRequest<Health>
    {
        public Guid InitiatorId { get; set; }
        public Guid CharacterId { get; set; }
        public int Change { get; set; }

        public ModifyHealthCommand(Guid initiatorId, Guid characterId, int change)
        {
            InitiatorId = initiatorId;
            CharacterId = characterId;
            Change = change;
        }
    }
}

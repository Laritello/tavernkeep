using MediatR;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollSkill
{
    public class RollSkillCommand : IRequest<SkillRollMessageDto>
    {
        public Guid InitiatorId { get; set; }
        public Guid CharacterId { get; set; }
        public SkillType SkillType { get; set; }
        public RollType RollType { get; set; }

        public RollSkillCommand(Guid initiatorId, Guid characterId, SkillType skillType, RollType rollType)
        {
            InitiatorId = initiatorId;
            CharacterId = characterId;
            SkillType = skillType;
            RollType = rollType;
        }
    }
}

using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSkill
{
    public class EditSkillCommand : IRequest<Skill>
    {
        public Guid InitiatorId { get; set; }
        public Guid CharacterId { get; set; }
        public SkillType Type { get; set; }
        public Proficiency Proficiency { get; set; }

        public EditSkillCommand(Guid initiatorId, Guid characterId, SkillType type, Proficiency proficiency)
        {
            InitiatorId = initiatorId;
            CharacterId = characterId;
            Type = type;
            Proficiency = proficiency;
        }
    }
}

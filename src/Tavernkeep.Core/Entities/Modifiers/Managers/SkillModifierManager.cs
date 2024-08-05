using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Conditions;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Modifiers.Managers
{
    public class SkillModifierManager : IModifierManager
    {
        private readonly Character _character;

        public SkillModifierManager(Character character, SkillType type)
        {
            _character = character;
            Target = type.ToTarget();
        }

        public ModifierTarget Target { get; init; }
        public IReadOnlyCollection<Condition> Conditions => _character.Conditions.AsReadOnly();

        public ModifierSummary GetSummary()
        {
            // TODO: Include item modifiers
            var conditionModifiers = Conditions
                .SelectMany(x => x.CollectModifiers(_character))
                .Where(x => x.Targets.Contains(Target))
                .ToList();

            var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
            var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MinBy(x => x.Value);

            var total = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);

            return new(conditionModifiers, total, activeBonus, activePenalty);
        }
    }
}

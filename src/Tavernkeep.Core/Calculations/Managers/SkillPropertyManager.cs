using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Calculations.Managers
{
    public class SkillPropertyManager(Character character, ModifierTarget target) : IPropertyManager
    {
        private readonly Character _character = character;
        private readonly ModifierTarget _target = target;

        public int GetBonus()
        {
            var conditions = _character.Conditions;

            var conditionModifiers = _character.Conditions
                .SelectMany(x => x.CollectModifiers(_character))
                .Where(x => x.Targets.Contains(_target))
                .ToList();

            var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
            var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

            var total = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);
            return total;
        }
    }
}

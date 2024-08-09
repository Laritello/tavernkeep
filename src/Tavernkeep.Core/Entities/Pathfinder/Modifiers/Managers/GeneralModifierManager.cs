using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Entities.Pathfinder.Modifiers.Managers
{
	public class GeneralModifierManager(Character character, ModifierTarget target) : IModifierManager
	{
		private readonly Character _character = character;

		public ModifierTarget Target { get; init; } = target;
		public IReadOnlyCollection<Condition> Conditions => _character.Conditions.AsReadOnly();

		public ModifierSummary GetSummary()
		{
			// TODO: Include item modifiers
			var conditionModifiers = Conditions
				.SelectMany(x => x.CollectModifiers(_character))
				.Where(x => x.Targets.Contains(Target))
				.ToList();

			var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
			var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

			var total = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);

			return new(conditionModifiers, total, activeBonus, activePenalty);
		}
	}
}

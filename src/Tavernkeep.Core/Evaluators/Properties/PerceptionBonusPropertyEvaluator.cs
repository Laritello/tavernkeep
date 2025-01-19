using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class PerceptionBonusPropertyEvaluator(Perception perception) : IValueEvaluator<int>
	{
		private readonly Perception _perception = perception;
		private readonly Character _character = perception.Owner;
		private readonly ModifierEvaluator _modifierEvaluator = new(perception.Owner, "Perception");

		public int Value => Calculate();

		private int Calculate()
		{
			return _character.Abilities["Wisdom"].Modifier + _perception.Proficiency.GetProficiencyBonus(_character) + _modifierEvaluator.Value;
		}
	}
}

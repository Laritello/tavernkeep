using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityFlaw;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class AbilityScorePropertyEvaluator(Ability ability) : IValueEvaluator<int>
	{
		private readonly Ability _ability = ability;
		private readonly Character _character = ability.Owner;

		public int Value => Calculate();

		private int Calculate()
		{
			int boostAmount = _character.Build.Attributes
				.Count(x => x.Level <= _character.Level && x is AbilityBoostAttribute a && a.Contains(_ability.Type));

			int flawAmount = _character.Build.Attributes
				.Count(x => x.Level <= _character.Level && x is AbilityFlawAttribute a && a.Contains(_ability.Type));

			int netAmount = boostAmount - flawAmount;
			int score = 10 + Math.Min(netAmount, 4) * 2 + Math.Max(netAmount - 4, 0);

			return score;
		}
	}
}

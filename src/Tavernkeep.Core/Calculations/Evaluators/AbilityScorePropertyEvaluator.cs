using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Calculations.Evaluators
{
	public class AbilityScorePropertyEvaluator(Ability ability) : IPropertyEvaluator<int>
	{
		private readonly Ability _ability = ability;
		private readonly Character _character = ability.Owner;

		public int Value => CalculateScore();

		private int CalculateScore()
		{
			int boostAmount = _character.Build.Advancements
				.Where(x => x.Level <= _character.Level && x is AbilityBoostAdvancement a && a.Selected == _ability.Type)
				.Count();

			int flawAmount = _character.Build.Advancements
				.Where(x => x.Level <= _character.Level && x is AbilityFlawAdvancement a && a.Selected == _ability.Type)
				.Count();

			int netAmount = boostAmount - flawAmount;
			int score = 10 + Math.Min(netAmount, 4) * 2 + Math.Max(netAmount - 4 , 0);

			return score;
		}
	}
}

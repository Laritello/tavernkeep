using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityFlaw;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Evaluators.Builds
{
	public class IntelligenceModifierEvaluator(List<AbilityModifierAttribute> template, List<BuildValue> snapshot) : IValueEvaluator<int>
	{
		public int Value => Calculate();

		private int Calculate()
		{
			List<BuildAttribute> attributes = [];

			foreach (var attribute in template)
			{
				attributes.Add(attribute.Convert(snapshot));
			}

			int boostAmount = attributes
				.Where(x => x is AbilityBoostAttribute a && a.Contains(AbilityType.Intelligence))
				.Count();

			int flawAmount = attributes
				.Where(x => x is AbilityFlawAttribute a && a.Contains(AbilityType.Intelligence))
				.Count();

			int netAmount = boostAmount - flawAmount;
			int score = 10 + Math.Min(netAmount, 4) * 2 + Math.Max(netAmount - 4, 0);

			return (score - 10) / 2;
		}
	}
}

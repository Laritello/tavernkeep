using d20Tek.DiceNotation.Results;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Application.Extensions
{
	public static class DiceExtensions
	{
		public static RollResult ToRollResult(this DiceResult result)
		{
			var rolls = result.Results.Where(x => !x.Type.Contains("ConstantTerm"));
			return new()
			{
				Value = result.Value,
				Modifier = result.Value - rolls.Sum(x => x.Value),
				Results = rolls.Select(x => new ThrowResult()
				{
					Value = x.Value,
					Type = x.Type.Replace("DiceTerm.", "")
				}).ToList()
			};
		}
	}
}

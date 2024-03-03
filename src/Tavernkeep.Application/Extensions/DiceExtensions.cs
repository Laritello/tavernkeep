using d20Tek.DiceNotation.Results;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Application.Extensions
{
    public static class DiceExtensions
    {
        public static RollResult ToRollResult(this DiceResult result)
        {
            return new()
            {
                Value = result.Value,
                Modifier = result.Value - result.Results.Sum(x => x.Value),
                Results = result.Results.Select(x => new ThrowResult()
                {
                    Value = x.Value,
                    Type = x.Type.Replace("DiceTerm.", "")
                }).ToList()
            };
        }
    }
}

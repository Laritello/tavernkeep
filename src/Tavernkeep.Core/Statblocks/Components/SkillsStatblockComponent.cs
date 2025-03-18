using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Templates;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class SkillsStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => DefaultStatblockTemplate.BuildFromTemplate(Tokens, IsDividerEnabled);

		public void ApplyConditions(IEnumerable<CreatureConditionRecord> records)
		{
			foreach (var skill in Tokens.Where(x => x is IModifiableSkillToken).Cast<IModifiableSkillToken>())
			{
				var evaluator = new CreatureModifierEvaluator(records, skill.Name);
				skill.Modifier = evaluator.Value;
			}
		}

		public IStatblockComponent ToModifiable()
		{
			return new SkillsStatblockComponent(Tokens.Select(x => x is IModifiableOriginToken origin ? origin.ToModifable() : x.Copy()).ToList())
			{
				IsDividerEnabled = IsDividerEnabled,
			};
		}
	}
}

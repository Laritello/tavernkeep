using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Templates;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class AttackStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => DefaultStatblockTemplate.BuildFromTemplate(Tokens, IsDividerEnabled);

		public void ApplyConditions(ICollection<ConditionRecord> records)
		{
			/* Algorithm:
			 * 1) Detect the applicable conditions
			 * 2) If resulting modifier is zero, stop.
			 * 3) Find all modifiable values
			 * 4) Change them and set flag for them to Buffed or Debuffed
			 */
			throw new NotImplementedException();
		}

		public IStatblockComponent Copy()
		{
			return new AttackStatblockComponent(Tokens.Select(x => x.Copy()).ToList())
			{
				IsDividerEnabled = IsDividerEnabled
			};
		}
	}
}

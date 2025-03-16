using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Templates;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class HealthStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }
		public int Health => 0;

		[JsonIgnore]
		public string HTML => DefaultStatblockTemplate.BuildFromTemplate(Tokens, IsDividerEnabled);

		public void ApplyConditions(ICollection<ConditionRecord> records)
		{
			throw new NotImplementedException();
		}

		public IStatblockComponent Copy()
		{
			return new HealthStatblockComponent(Tokens.Select(x => x.Copy()).ToList())
			{
				IsDividerEnabled = IsDividerEnabled,
			};
		}
	}
}

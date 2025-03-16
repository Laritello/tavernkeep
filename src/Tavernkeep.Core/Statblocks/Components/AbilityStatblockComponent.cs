using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Templates;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class AbilityStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => DefaultStatblockTemplate.BuildFromTemplate(Tokens, IsDividerEnabled);

		public void ApplyConditions(ICollection<ConditionRecord> records)
		{
			throw new NotImplementedException();
		}

		public IStatblockComponent Copy()
		{
			throw new NotImplementedException();
		}
	}
}

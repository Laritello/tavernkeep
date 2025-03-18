using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Templates;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class LanguagesStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => DefaultStatblockTemplate.BuildFromTemplate(Tokens, IsDividerEnabled);

		public void ApplyConditions(IEnumerable<CreatureConditionRecord> records)
		{
			throw new NotImplementedException();
		}

		public IStatblockComponent ToModifiable()
		{
			return new LanguagesStatblockComponent(Tokens.Select(x => x is IModifiableOriginToken origin ? origin.ToModifable() : x.Copy()).ToList())
			{
				IsDividerEnabled = IsDividerEnabled,
			};
		}
	}
}

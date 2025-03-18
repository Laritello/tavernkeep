using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Tokens;
using Tavernkeep.Core.Statblocks.Tokens.Modifiable;

namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	[JsonDerivedType(typeof(ActionToken), nameof(ActionToken))]
	[JsonDerivedType(typeof(BonusToken), nameof(BonusToken))]
	[JsonDerivedType(typeof(KeywordToken), nameof(KeywordToken))]
	[JsonDerivedType(typeof(ListItemToken), nameof(ListItemToken))]
	[JsonDerivedType(typeof(ListToken), nameof(ListToken))]
	[JsonDerivedType(typeof(SkillToken), nameof(SkillToken))]
	[JsonDerivedType(typeof(TextToken), nameof(TextToken))]
	[JsonDerivedType(typeof(ModifiableBonusToken), nameof(ModifiableBonusToken))]
	[JsonDerivedType(typeof(ModifiableSkillToken), nameof(ModifiableSkillToken))]
	public interface IToken
	{
		public string Html { get; }
		public IToken Copy();
	}
}

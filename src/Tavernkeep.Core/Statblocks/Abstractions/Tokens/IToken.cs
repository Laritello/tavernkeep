using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Tokens;

namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	[JsonDerivedType(typeof(ActionToken), nameof(ActionToken))]
	[JsonDerivedType(typeof(TextToken), nameof(TextToken))]
	[JsonDerivedType(typeof(KeywordToken), nameof(KeywordToken))]
	[JsonDerivedType(typeof(ListToken), nameof(ListToken))]
	[JsonDerivedType(typeof(ListItemToken), nameof(ListItemToken))]
	public interface IToken
	{
		public string Html { get; }
	}
}

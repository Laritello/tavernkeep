using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens.Modifiable
{
	public class ModifiableBonusToken(int bonus, int modifier = 0) : IBonusToken, IModifiableToken
	{
		public int Bonus { get; set; } = bonus;
		public int Modifier { get; set; } = modifier;
		[JsonIgnore]
		public string Html => $"{(Bonus >= 0 ? "+" : "")}{Bonus}";

		public IToken Copy()
		{
			return new ModifiableBonusToken(Bonus, Modifier);
		}
	}
}

using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Tokens.Modifiable;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class BonusToken(int bonus) : IBonusToken, IModifiableOriginToken
	{
		public int Bonus { get; set; } = bonus;
		[JsonIgnore]
		public string Html => $"{(Bonus >= 0 ? "+" : "")}{Bonus}";

		public IToken Copy()
		{
			return new BonusToken(Bonus);
		}

		public IToken ToModifable()
		{
			return new ModifiableBonusToken(Bonus);
		}
	}
}

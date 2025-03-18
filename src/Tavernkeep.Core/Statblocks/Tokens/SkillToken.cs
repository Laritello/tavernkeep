using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Tokens.Modifiable;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class SkillToken(string name, int bonus) : ISkillToken, IModifiableOriginToken
	{
		public string Name { get; set; } = name;
		public int Bonus { get; set; } = bonus;

		[JsonIgnore]
		public string Html => $"{Name} {(Bonus >= 0 ? "+" : "")}{Bonus}";

		public IToken Copy()
		{
			return new SkillToken(Name, Bonus);
		}

		public IToken ToModifable()
		{
			return new ModifiableSkillToken(Name, Bonus);
		}
	}
}

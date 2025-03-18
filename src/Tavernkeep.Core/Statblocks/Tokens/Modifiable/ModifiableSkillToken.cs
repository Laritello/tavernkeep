using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens.Modifiable
{
	public class ModifiableSkillToken(string name, int bonus, int modifier = 0) : IModifiableSkillToken
	{
		public string Name { get; set; } = name;
		public int Bonus { get; set; } = bonus;
		public int Modifier { get; set; } = modifier;

		[JsonIgnore]
		public string Html => Modifier == 0
			? $"{Name} {(Bonus >= 0 ? "+" : "")}{Bonus}"
			: $"<span>{Name} <span class='{(Modifier > 0 ? "positive" : "negative")}'>{(Bonus + Modifier >= 0 ? "+" : "")}{Bonus + Modifier}</span></span>";

		public IToken Copy()
		{
			return new ModifiableSkillToken(Name, Bonus, Modifier);
		}
	}
}

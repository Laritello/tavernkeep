using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class SkillToken(string name, int bonus) : ISkillToken
	{
		public string Name { get; set; } = name;
		public int Bonus { get; set; } = bonus;

		public string Html => $"{Name} {(Bonus >= 0 ? "+" : "")}{Bonus}";

		public IToken Copy()
		{
			return new SkillToken(Name, Bonus);
		}
	}
}

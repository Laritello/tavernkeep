using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class BonusToken(int bonus) : IBonusToken
	{
		public int Bonus { get; set; } = bonus;
		public string Html => $"{(Bonus >= 0 ? "+" : "")}{Bonus}";

		public IToken Copy()
		{
			return new BonusToken(Bonus);
		}
	}
}

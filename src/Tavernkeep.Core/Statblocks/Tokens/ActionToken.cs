using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class ActionToken(ActionAmount amount) : IActionToken
	{
		public ActionAmount Amount { get; set; } = amount;

		public string Html
		{
			get
			{
				return Amount switch
				{
					ActionAmount.Free => "<img class='text-img pf-action-free'></img>",
					ActionAmount.Reaction => "<img class='text-img pf-action-reaction'></img>",
					ActionAmount.One => "<img class='text-img pf-action-one'></img>",
					ActionAmount.Two => "<img class='text-img pf-action-two'></img>",
					ActionAmount.Three => "<img class='text-img pf-action-three'></img>",
					_ => throw new NotImplementedException()
				};
			}
		}

		public IToken Copy()
		{
			return new ActionToken(Amount);
		}
	}
}

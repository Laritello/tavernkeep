using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class ActionToken : IActionToken
	{
		public required ActionAmount Amount { get; set; }

		public string Html
		{
			get
			{
				return Amount switch
				{
					ActionAmount.Free => "<img src='/assets/images/pf-action-free.png' class='text-img'></img>",
					ActionAmount.Reaction => "<img src='/assets/images/pf-action-reaction.png' class='text-img'></img>",
					ActionAmount.One => "<img src='/assets/images/pf-action-1.png' class='text-img'></img>",
					ActionAmount.Two => "<img src='/assets/images/pf-action-2.png' class='text-img'></img>",
					ActionAmount.Three => "<img src='/assets/images/pf-action-3.png' class='text-img'></img>",
					_ => throw new NotImplementedException()
				};
			}
		}
	}
}

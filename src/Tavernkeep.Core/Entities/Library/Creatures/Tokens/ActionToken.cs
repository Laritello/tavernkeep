using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens;

namespace Tavernkeep.Core.Entities.Library.Creatures.Tokens
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
					ActionAmount.Free => "<img src='./resources/pf-action-free.png' class='text-img'></img>",
					ActionAmount.Reaction => "<img src='./resources/pf-action-reaction.png' class='text-img'></img>",
					ActionAmount.One => "<img src='./resources/pf-action-1.png' class='text-img'></img>",
					ActionAmount.Two => "<img src='./resources/pf-action-2.png' class='text-img'></img>",
					ActionAmount.Three => "<img src='./resources/pf-action-3.png' class='text-img'></img>",
					_ => throw new NotImplementedException()
				};
			}
		}
	}
}

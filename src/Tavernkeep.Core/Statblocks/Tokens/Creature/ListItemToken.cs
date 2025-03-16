using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class ListItemToken : IListItemToken
	{
		public bool IsStart { get; set; }

		public string Html => IsStart ? "<li>" : "</li>";

		public IToken Copy()
		{
			return new ListItemToken() { IsStart = IsStart };
		}
	}
}

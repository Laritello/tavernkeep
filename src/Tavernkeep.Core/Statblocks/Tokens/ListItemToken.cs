using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class ListItemToken(bool isStart) : IListItemToken
	{
		public bool IsStart { get; set; } = isStart;

		public string Html => IsStart ? "<li>" : "</li>";

		public IToken Copy()
		{
			return new ListItemToken(IsStart);
		}
	}
}

using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class ListToken(bool isStart) : IListToken
	{
		public bool IsStart { get; set; } = isStart;

		public string Html => IsStart ? "<ul>" : "</ul>";

		public IToken Copy()
		{
			return new ListToken(IsStart);
		}
	}
}

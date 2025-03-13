using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class ListToken : IListToken
	{
		public bool IsStart { get; set; }

		public string Html => IsStart ? "<ul>" : "</ul>";
	}
}

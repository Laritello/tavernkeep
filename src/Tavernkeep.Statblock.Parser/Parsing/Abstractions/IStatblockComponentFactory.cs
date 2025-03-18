using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Abstractions;

namespace Tavernkeep.Statblock.Parser.Parsing.Abstractions
{
	public interface IStatblockComponentFactory
	{
		public IStatblockComponent GetComponent(List<IToken> source, ref int i);
	}
}

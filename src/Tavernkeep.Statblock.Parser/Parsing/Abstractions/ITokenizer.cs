using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Statblock.Parser.Parsing.Abstractions
{
	public interface ITokenizer<T>
	{
		public List<IToken> Tokenize(T source);
	}
}

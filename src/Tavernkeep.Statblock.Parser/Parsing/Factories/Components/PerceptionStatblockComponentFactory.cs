using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Components;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;
using Tavernkeep.Statblock.Parser.Utilities;

namespace Tavernkeep.Statblock.Parser.Parsing.Factories.Components
{
	internal sealed class PerceptionStatblockComponentFactory : IStatblockComponentFactory
	{
		public IStatblockComponent GetComponent(List<IToken> source, ref int i)
		{
			var tokens = ParsingUtilities.Collect(source, ref i, source.Count);
			return new PerceptionStatblockComponent(tokens);
		}
	}
}

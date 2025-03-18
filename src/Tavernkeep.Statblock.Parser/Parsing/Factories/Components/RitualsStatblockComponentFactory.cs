using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Components;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;
using Tavernkeep.Statblock.Parser.Utilities;
using Tavernkeep.Core.Statblocks;

namespace Tavernkeep.Statblock.Parser.Parsing.Factories.Components
{
	internal sealed class RitualsStatblockComponentFactory : IStatblockComponentFactory
	{
		public IStatblockComponent GetComponent(List<IToken> source, ref int i)
		{
			var token = ParsingUtilities.Collect(source, ref i, source.Count, s => StatblockRegexes.SpellsLevel().IsMatch(s) || StatblockRegexes.SpellsKeywords().IsMatch(s));
			return new RitualsStatblockComponent(token);
		}
	}
}

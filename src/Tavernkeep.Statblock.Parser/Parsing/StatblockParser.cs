using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Entities.Library.Creatures;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Components;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;
using Tavernkeep.Statblock.Parser.Parsing.Factories;

namespace Tavernkeep.Statblock.Parser.Parsing
{
	public class StatblockParser<T> where T : class, new()
	{
		public ITokenizer<T> ParsingStrategy { get; set; } = TokenizerFactory.GetTokenizer<T>();

		public Creature Parse(T source)
		{
			Creature creature = new() { Name = string.Empty, Type = "CREATURE" };

			var tokens = ParsingStrategy.Tokenize(source);

			int length = tokens.Count;

			creature.AddBlock(new GeneralStatblockComponent([tokens[0]]));
			creature.AddBlock(new TraitsStatblockComponent([tokens[1]]));

			int i = 2;

			while (i < length)
			{
				if (tokens[i] is IKeywordToken keyword)
				{
					var factory = ParsingStrategyFactory.GetFactory(keyword.Name);
					var component = factory.GetComponent(tokens, ref i);
					creature.AddBlock(component);
				}
				else
				{
					i++;
				}
			}

			/** Note: lists inside section
			 * Somtimes block contains list with subsections
			 * Example: Chimera
			 */
			/** Note: list with multiple keywords
			 * Example: CACODAEMON
			 */
			/** Note: overflow for traits
			 * Example: JANN
			 */
			/** Note: Hydra health case
			 * It has multiple health values
			 */

			creature.Name = creature.Blocks.First(x => x is GeneralStatblockComponent).Cast<GeneralStatblockComponent>().Name;
			creature.Type = creature.Blocks.First(x => x is GeneralStatblockComponent).Cast<GeneralStatblockComponent>().Type;
			creature.Level = creature.Blocks.First(x => x is GeneralStatblockComponent).Cast<GeneralStatblockComponent>().Level;
			creature.Traits = creature.Blocks.First(x => x is TraitsStatblockComponent).Cast<TraitsStatblockComponent>().Traits;

			return creature;
		}
	}
}

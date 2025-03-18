using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	public interface IModifiableToken : IToken
	{
		public int Modifier { get; set; }
	}
}

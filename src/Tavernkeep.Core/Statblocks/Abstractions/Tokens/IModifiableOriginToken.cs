namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	public interface IModifiableOriginToken : IToken
	{
		public IToken ToModifable();
	}
}

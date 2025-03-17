namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	public interface ISkillToken : IToken
	{
		public string Name { get; set; }
		public int Bonus { get; set; }
	}
}

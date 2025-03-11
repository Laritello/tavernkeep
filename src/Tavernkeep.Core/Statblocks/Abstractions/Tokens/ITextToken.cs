namespace Tavernkeep.Core.Statblocks.Abstractions.Tokens
{
	public interface ITextToken : IToken, IAttachableToken
	{
		public string Text { get; set; }
		public bool IsBold { get; set; }
		public bool IsItalic { get; set; }
	}
}

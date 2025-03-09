namespace Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens
{
	public interface ITextToken : IToken, IAttachableToken
	{
		public string Text { get; set; }
		public bool IsBold { get; set; }
		public bool IsItalic { get; set; }
	}
}

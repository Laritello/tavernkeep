using Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Library.Creatures.Tokens
{
	public class TextToken(string text, bool isBold = false, bool isItalic = false) : ITextToken
	{
		public string Text { get; set; } = text;
		public bool IsBold { get; set; } = isBold;
		public bool IsItalic { get; set; } = isItalic;

		public string Html
		{
			get
			{
				string html = Text;

				if (IsBold)
				{
					html = html.HtmlWrapIn("strong");
				}

				if (IsItalic)
				{
					html = html.HtmlWrapIn("em");
				}

				return html;
			}
		}

		public bool IsAttachable(IToken token)
		{
			if (token is not ITextToken textToken)
			{
				return false;
			}

			return textToken.IsBold == IsBold && textToken.IsItalic == IsItalic;
		}

		public void Attach(IToken token)
		{
			if (token is not ITextToken textToken || !IsAttachable(token))
			{
				throw new ArgumentException("Token format is not supported", nameof(token));
			}

			Text += $" {textToken.Text}";
		}
	}
}

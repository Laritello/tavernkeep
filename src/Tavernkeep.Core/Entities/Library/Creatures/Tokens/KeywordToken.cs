using Tavernkeep.Core.Entities.Library.Creatures.Abstractions.Tokens;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Library.Creatures.Tokens
{
	public class KeywordToken : IKeywordToken
	{
		public required string Name { get; set; }
		public string Html
		{
			get
			{
				return $"{Name.HtmlWrapIn("strong")}";
			}
		}

		public void Attach(IToken token)
		{
			if (token is not IKeywordToken keywordToken)
			{
				throw new ArgumentException("Token format is not supported", nameof(token));
			}

			Name += $" {keywordToken.Name}";
		}

		public bool IsAttachable(IToken token)
		{
			return token is IKeywordToken;
		}
	}
}

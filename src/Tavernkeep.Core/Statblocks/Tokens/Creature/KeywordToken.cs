using System.Text.Json.Serialization;
using Tavernkeep.Core.Extensions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Tokens
{
	public class KeywordToken : IKeywordToken
	{
		public required string Name { get; set; }
		[JsonIgnore]
		public bool IsCheckResult => Name is "Critical Success" or "Critical Failure" or "Success" or "Failure";
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

		public IToken Copy()
		{
			return new KeywordToken() { Name = Name };
		}

		public bool IsAttachable(IToken token)
		{
			return token is IKeywordToken;
		}
	}
}

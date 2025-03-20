using System.Text.RegularExpressions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;
using Tavernkeep.Core.Statblocks.Tokens;
using Tavernkeep.Core.Statblocks;
using Tavernkeep.Statblock.Parser.Entities;
using Tavernkeep.Statblock.Parser.Parsing.Abstractions;
using Tavernkeep.Statblock.Parser.Enums;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Tavernkeep.Statblock.Parser.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace Tavernkeep.Statblock.Parser.Parsing.Tokenizers
{
	internal class PathbuilderTokenizer : ITokenizer<PathbuilderRecord>
	{
		public List<IToken> Tokenize(PathbuilderRecord source)
		{
			var html = Regex.Replace(source.Statblock, @"\t|\n|\r", "");
			html = html.Replace("[free action]", "[free-action]");
			html = html.Replace("â\u0080\u0093", "-");

			var parser = new HtmlParser();
			var document = parser.ParseDocument(html);

			var tokens = new List<IToken>();
			ExtractTokens(document.DocumentElement, tokens);

			var traits = source.Traits.Split(',').Select(x => x.Trim()).ToList();
			var rarity = traits.FirstOrDefault(x => x.ToLower() is "uncommon" or "rare" or "unique") ?? string.Empty;

			if (source.Alignment is null)
			{
				throw new InvalidOperationException();
			}

			traits.Insert(0, source.Size);
			traits.Insert(0, source.Alignment);

			if (rarity != string.Empty)
			{
				traits.Remove(rarity);
				traits.Insert(0, rarity);
			}

			TextToken headerBlock = new($"{source.Name} CREATURE {source.Level}");
			TextToken traitsBlock = new(string.Join(" ", traits));

			return [headerBlock, traitsBlock, .. tokens];
		}

		private static void ExtractTokens(INode node, List<IToken> tokens)
		{

			// Traverse the DOM tree
			foreach (var childNode in node.ChildNodes)
			{
				if (childNode is IHtmlUnorderedListElement listElement)
				{
					tokens.Add(new ListToken(true));
					ExtractTokens(listElement, tokens);
					tokens.Add(new ListToken(false));
				}
				else if (childNode is IHtmlListItemElement listItemElement)
				{
					tokens.Add(new ListItemToken(true));
					ExtractTokens(listItemElement, tokens);
					tokens.Add(new ListItemToken(false));
				}
				else if (childNode is IText textNode && !string.IsNullOrWhiteSpace(textNode.TextContent))
				{
					var words = textNode.TextContent.Split(' ');
					var localTokens = new List<IToken>();

					int i = 0;

					while (i < words.Length)
					{
						var currentTokenText = words[i].Trim();
						var nextTokenText = i < words.Length - 1 ? words[i + 1].Trim() : string.Empty;

						if (string.IsNullOrWhiteSpace(currentTokenText))
						{
							i++;
							continue;
						}

						var tokenType = GetTokenType(currentTokenText, nextTokenText, IsBold(childNode.ParentElement));

						IToken token = tokenType switch
						{
							TokenType.Text => new TextToken(currentTokenText, IsBold(childNode.ParentElement), IsItalic(childNode.ParentElement)),
							TokenType.Skill => new SkillToken(currentTokenText, GetSkillModifier(nextTokenText)),
							TokenType.Action => new ActionToken(currentTokenText.ToActionAmount()),
							TokenType.Keyword => new KeywordToken(currentTokenText),
							TokenType.Bonus => new BonusToken(int.Parse(currentTokenText)),
							_ => throw new NotImplementedException()
						};

						if (tokens.LastOrDefault() is IAttachableToken attachableToken && attachableToken.IsAttachable(token))
						{
							attachableToken.Attach(token);
						}
						else
						{
							tokens.Add(token);
						}

						// Handle cases where modifier ends with a comma
						if (tokenType is TokenType.Skill && GetSkillLeftText(nextTokenText, out string skillText))
						{
							tokens.Add(new TextToken(skillText));
						}

						switch (tokenType)
						{
							case TokenType.Skill:
								i += 2;
								break;
							default:
								i++;
								break;
						}
					}
				}
				else if (childNode is IElement elementNode)
				{
					// Recursively process child elements
					ExtractTokens(elementNode, tokens);
				}
			}
		}

		private static TokenType GetTokenType(string current, string next, bool isBold)
		{
			return (current, next, isBold) switch
			{
				(string t, string n, _) when StatblockRegexes.SkillName().IsMatch(t) && StatblockRegexes.ModifierWithText().IsMatch(n) => TokenType.Skill,
				(string t, _, _) when StatblockRegexes.ActionsKeywords().IsMatch(t) => TokenType.Action,
				(string t, _, _) when StatblockRegexes.Modifier().IsMatch(t) => TokenType.Bonus,
				(_, _, bool b) when b => TokenType.Keyword,
				_ => TokenType.Text
			};
		}

		private static int GetSkillModifier(string t)
		{
			var text = StatblockRegexes.ModifierWithText().Match(t).Groups["modifier"].Value;
			return int.Parse(text);
		}

		private static bool GetSkillLeftText(string t, out string text)
		{
			text = StatblockRegexes.ModifierWithText().Match(t).Groups["text"].Value;
			return !string.IsNullOrWhiteSpace(text);
		}

		private static bool IsBold(IElement? element)
		{
			// Check if the element is a bold tag (<b>, <strong>, etc.)
			return element != null && (element.TagName == "B" || element.TagName == "STRONG");
		}

		private static bool IsItalic(IElement? element)
		{
			// Check if the element is an italic tag (<i>, <em>, etc.)
			return element != null && (element.TagName == "I" || element.TagName == "EM");
		}

		private static bool IsListItem(IElement? element)
		{
			// Check if the element is a list item tag (<li>)
			return element != null && element.TagName == "LI";
		}
	}
}

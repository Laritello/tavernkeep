using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Templates
{
	public static class DefaultStatblockTemplate
	{
		public static string BuildFromTemplate(List<IToken> tokens, bool divider)
		{
			HtmlDocument document = new();
			HtmlNode parent = document.DocumentNode;
			document.OptionWriteEmptyNodes = true;

			var paragraph = HtmlNode.CreateNode("<p class='hang'></p>");
			parent.AppendChild(paragraph);

			int i = 0;
			while (i < tokens.Count)
			{
				var token = tokens[i];

				if (token is IKeywordToken keyword && keyword.IsCheckResult)
				{
					paragraph = HtmlNode.CreateNode("<p class='hang-nested'></p>");
					parent.AppendChild(paragraph);
				}

				var html = token switch
				{
					IListToken t when t.IsStart => BuildList(tokens, ref i),
					_ => token.Html
				};

				paragraph.AppendChild(HtmlNode.CreateNode(html));
				paragraph.AppendChild(HtmlNode.CreateNode(" "));
				i++;
			}

			if (divider)
			{
				parent.AppendChild(HtmlNode.CreateNode("<div class='divider'></div>"));
			}

			return parent.OuterHtml;
		}

		private static string BuildList(List<IToken> tokens, ref int i)
		{
			var end = tokens.Skip(i).FirstOrDefault(x => x is IListToken list && list.IsStart == false)
							?? throw new Exception("No ending section for list");

			var html = string.Empty;

			while (i < tokens.Count)
			{
				html += $"{tokens[i].Html} ";

				if (tokens[i] == end)
				{
					break;
				}

				i++;
			}

			return html;
		}
	}
}

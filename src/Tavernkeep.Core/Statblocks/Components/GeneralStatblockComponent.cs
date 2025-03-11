using HtmlAgilityPack;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class GeneralStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			if (Tokens.Count == 0 || Tokens[0] is not ITextToken text)
			{
				return string.Empty;
			}

			var result = StatblockRegexes.GeneralInfromation().Match(text.Text);

			var name = result.Groups["creatureName"].Value.Trim();
			var type = result.Groups["creatureType"].Value;
			var level = int.Parse(result.Groups["creatureLevel"].Value.Replace('–', '-'));

			HtmlDocument document = new();
			HtmlNode parent = document.DocumentNode;

			var div = HtmlNode.CreateNode($@"<div class='flex flex-row'><h1 class='flex-1'>{name}</h1><h1>{type} {level}</h1></div>");
			parent.AppendChild(div);

			if (IsDividerEnabled)
			{
				parent.AppendChild(HtmlNode.CreateNode("<div class='divider'></div>"));
			}

			return parent.OuterHtml;
		}
	}
}

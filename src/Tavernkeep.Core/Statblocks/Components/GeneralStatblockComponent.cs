using HtmlAgilityPack;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class GeneralStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		#region Backing fields

		private string? _name;
		private string? _type;
		private int? _level;

		#endregion

		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		public string Name => _name ??= GetName();
		public string Type => _type ??= GetCreatureType();
		public int Level => _level ??= GetLevel();

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

		private string GetName()
		{
			if (Tokens.Count == 0 || Tokens[0] is not ITextToken text)
			{
				return string.Empty;
			}

			var result = StatblockRegexes.GeneralInfromation().Match(text.Text);
			return result.Groups["creatureName"].Value.Trim();
		}

		private string GetCreatureType()
		{
			if (Tokens.Count == 0 || Tokens[0] is not ITextToken text)
			{
				return string.Empty;
			}

			var result = StatblockRegexes.GeneralInfromation().Match(text.Text);
			return result.Groups["creatureType"].Value;
		}

		private int GetLevel()
		{
			if (Tokens.Count == 0 || Tokens[0] is not ITextToken text)
			{
				return 0;
			}

			var result = StatblockRegexes.GeneralInfromation().Match(text.Text);
			return int.Parse(result.Groups["creatureLevel"].Value.Replace('–', '-'));
		}

		public IStatblockComponent Copy()
		{
			return new GeneralStatblockComponent(Tokens.Select(x => x.Copy()).ToList())
			{
				IsDividerEnabled = IsDividerEnabled,
			};
		}

		public void ApplyConditions(ICollection<ConditionRecord> records)
		{
			throw new NotImplementedException();
		}
	}
}

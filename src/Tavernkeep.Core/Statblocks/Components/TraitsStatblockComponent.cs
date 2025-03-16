using Scriban;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Statblocks.Abstractions;
using Tavernkeep.Core.Statblocks.Abstractions.Tokens;

namespace Tavernkeep.Core.Statblocks.Components
{
	public sealed class TraitsStatblockComponent(List<IToken> tokens) : IStatblockComponent
	{
		private readonly string html_template = @"<div class='flex flex-row flex-wrap pt-1 max-w-full'>{{ for trait in traits }}<div class='pf-trait {{ trait.bonus }}'>{{ trait.name }}</div>{{ end }}</div>";
		public List<IToken> Tokens { get; set; } = tokens;
		public bool IsDividerEnabled { get; set; }

		[JsonIgnore]
		public string HTML => GenerateHTML();

		private string GenerateHTML()
		{
			if (Tokens.Count == 0)
			{
				return string.Empty;
			}

			var traits = Tokens.Cast<ITextToken>().First().Text.Split(' ');

			var template = Template.Parse(html_template);
			var html = template.Render(new { Traits = traits.Select(x => new { Name = x, Bonus = GetBonusStyleClass(x) }) });

			if (IsDividerEnabled)
			{
				html = html + Environment.NewLine + "<div class='divider'></div>";
			}

			return html;
		}

		private static string GetBonusStyleClass(string trait)
		{
			return trait.ToLower() switch
			{
				"tiny" or "small" or "medium" or "large" or "huge" or "gargantuan" => "pf-trait-size",
				"lg" or "ng" or "cg" or "ln" or "n" or "cn" or "le" or "ne" or "ce" => "pf-trait-align",
				"uncommon" => "pf-trait-uncommon",
				"rare" => "pf-trait-rare",
				"unique" => "pf-trait-unique",
				_ => string.Empty
			};
		}

		public IStatblockComponent Copy()
		{
			return new TraitsStatblockComponent(Tokens.Select(x => x.Copy()).ToList())
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

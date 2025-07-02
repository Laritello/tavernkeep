using System.Text.RegularExpressions;

namespace Tavernkeep.Utility.Parser.Html.Processors;

internal partial class RollableProcessor : IHtmlProcessor
{
	public string Process(string html)
	{
		html = Abilities().Replace(html, match =>
		{
			string name = match.Groups[1].Value;
			return Modifier().Replace(match.Value, m => $"<span class=\"rollable\" data-type=\"ability\" data-name=\"{name}\">{m.Groups[1].Value}</span>");
		});

		html = Saves().Replace(html, match =>
		{
			string name = match.Groups[1].Value;
			return Modifier().Replace(match.Value, m => $"<span class=\"rollable\" data-type=\"save\" data-name=\"{name}\">{m.Groups[1].Value}</span>");
		});

		html = Attack().Replace(html, match =>
		{
			var type = match.Groups[1].Value;
			var name = match.Groups[3].Value;
			return Modifier().Replace(match.Value, m => $"<span class=\"rollable\" data-type=\"{type}\" data-name=\"{name}\">{m.Groups[1].Value}</span>");
		});

		html = Skills().Replace(html, match =>
		{
			var type = match.Groups[1].Value;
			return Modifier().Replace(match.Value, m => $"<span class=\"rollable\" data-type=\"skill\" data-name=\"{type}\">{m.Groups[1].Value}</span>");
		});

		return html;

		// TODO: Найти Г-З и что это такое в Pathbuilder. Если надо - исправить Xulgath Deepmouth
		// TODO: в названии атак исправить, что пробел съедает остальное название атаки. Например, wing shard в data-name лежит только wing
	}

	[GeneratedRegex(@"<b>(Str|Dex|Con|Int|Wis|Cha)<\/b>\s*([+-]\d+)")]
	private static partial Regex Abilities();

	[GeneratedRegex(@"<b>(Fort|Ref|Will)<\/b>\s*([+-]\d+)")]
	private static partial Regex Saves();

	[GeneratedRegex(@"(Acrobatics|Arcana|Athletics|Crafting|Deception|Diplomacy|Intimidation|Medicine|Nature|Occultism|Performance|Religion|Society|Stealth|Survival|Thievery|Lore)\s*([+-]\d+)(?:\s*\(([^)]+)\))?")]
	private static partial Regex Skills();

	[GeneratedRegex(@"<b>(Melee|Ranged)<\/b>\s+\[(.*?)\]\s+(.*?(?=\s))\s+(.*?(?=\])\])")]
	private static partial Regex Attack();

	[GeneratedRegex(@"((?:-|\+)\d+)")]
	private static partial Regex Modifier();
}

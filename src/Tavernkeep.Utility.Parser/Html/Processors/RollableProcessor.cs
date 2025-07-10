using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Tavernkeep.Utility.Parser.Html.Processors;

internal partial class RollableProcessor : IHtmlProcessor
{
	public string Process(string html)
	{
		html = Abilities().Replace(html, match =>
		{
			string name = ToAbilityName(match.Groups[1].Value.ToLower());
			return Modifier().Replace(match.Value, m => $"<span class=\"statblock-item\" data-type=\"ability\" data-name=\"{name}\">{m.Groups[1].Value}</span>");
		});

		html = Saves().Replace(html, match =>
		{
			string name = ToSaveName(match.Groups[1].Value.ToLower());
			return Modifier().Replace(match.Value, m => $"<span class=\"statblock-item rollable\" data-type=\"save\" data-name=\"{name}\">{m.Groups[1].Value}</span>");
		});

		html = Attack().Replace(html, match =>
		{
			var type = match.Groups[1].Value.ToLower();
			var name = FirstCharToUpper(match.Groups[3].Value.Trim());
			return Modifier().Replace(match.Value, m => $"<span class=\"statblock-item rollable\" data-type=\"{type}\" data-name=\"{name}\">{m.Groups[1].Value}</span>");
		});

		html = Skills().Replace(html, match =>
		{
			var type = match.Groups[1].Value;
			return Modifier().Replace(match.Value, m => $"<span class=\"statblock-item rollable\" data-type=\"skill\" data-name=\"{type}\">{m.Groups[1].Value}</span>");
		});

		html = ArmorClass().Replace(html, match =>
		{
			return Number().Replace(match.Value, m => $"<span class=\"statblock-item\" data-type=\"armor\">{m.Groups[1].Value}</span>");
		});

		return html;

		// TODO: Найти Г-З и что это такое в Pathbuilder. Если надо - исправить Xulgath Deepmouth
	}

	private static string ToAbilityName(string shortVersion) => shortVersion switch
	{
		"str" => "Strength",
		"dex" => "Dexterity",
		"int" => "Intelligence",
		"con" => "Constitution",
		"wis" => "Wisdom",
		"cha" => "Charisma",
		_ => throw new SwitchExpressionException(shortVersion)
	};

	private static string ToSaveName(string shortVersion) => shortVersion switch
	{
		"fort" => "Fortitude",
		"ref" => "Reflex",
		"will" => "Will",
		_ => throw new SwitchExpressionException(shortVersion)
	};

	private static string FirstCharToUpper(string input) =>
		input switch
		{
			null => throw new ArgumentNullException(nameof(input)),
			"" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
			_ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
		};

	[GeneratedRegex(@"<b>AC<\/b>(.*?);")]
	private static partial Regex ArmorClass();

	[GeneratedRegex(@"<b>(Str|Dex|Con|Int|Wis|Cha)<\/b>\s*([+-]\d+)")]
	private static partial Regex Abilities();

	[GeneratedRegex(@"<b>(Fort|Ref|Will)<\/b>\s*([+-]\d+)")]
	private static partial Regex Saves();

	[GeneratedRegex(@"(Acrobatics|Arcana|Athletics|Crafting|Deception|Diplomacy|Intimidation|Medicine|Nature|Occultism|Performance|Religion|Society|Stealth|Survival|Thievery|Lore)\s*([+-]\d+)(?:\s*\(([^)]+)\))?")]
	private static partial Regex Skills();

	[GeneratedRegex(@"<b>(Melee|Ranged)<\/b>\s+\[(.*?)\]\s+(.*?(?=\+))(.*?(?=\])\])")]
	private static partial Regex Attack();

	[GeneratedRegex(@"((?:-|\+)\d+)")]
	private static partial Regex Modifier();

	[GeneratedRegex(@"(\d+)")]
	private static partial Regex Number();
}

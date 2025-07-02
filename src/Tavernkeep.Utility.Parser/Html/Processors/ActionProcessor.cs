using System.Text.RegularExpressions;

namespace Tavernkeep.Utility.Parser.Html.Processors;

internal partial class ActionProcessor : IHtmlProcessor
{
	private static readonly Dictionary<string, string> _actionMappings = new(StringComparer.OrdinalIgnoreCase)
	{
		["reaction"] = "reaction",
		["free-action"] = "free",
		["one-action"] = "one",
		["two-actions"] = "two",
		["three-actions"] = "three"
	};

	public string Process(string html)
	{
		return ActionNotation().Replace(html, match =>
		{
			string actionType = match.Groups[1].Value.ToLower();
			return _actionMappings.TryGetValue(actionType, out var v) ? $"<img class='text-img pf-action-{v}'/>" : match.Value;
		});
	}

	[GeneratedRegex(@"\[(free-action|reaction|one-action|two-actions|three-actions)\]")]
	private static partial Regex ActionNotation();
}
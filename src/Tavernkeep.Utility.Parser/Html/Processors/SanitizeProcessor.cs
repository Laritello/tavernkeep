using System.Text.RegularExpressions;

namespace Tavernkeep.Utility.Parser.Html.Processors;

internal partial class SanitizeProcessor : IHtmlProcessor
{
	public string Process(string html)
	{
		html = RemoveSpecialCharacters().Replace(html, "");
		html = html.Replace("[free action]", "[free-action]");
		html = html.Replace("â\u0080\u0093", "-");
		html = html.Replace("–", "-");
		html = html.Replace("x", "x");
		html = html.Replace("×", "x");
		return html;
	}

	[GeneratedRegex(@"\t|\n|\r")]
	private static partial Regex RemoveSpecialCharacters();
}
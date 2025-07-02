namespace Tavernkeep.Utility.Parser.Html.Processors;

internal class DividerProcessor : IHtmlProcessor
{
	public string Process(string html)
	{
		var armorDivider = FindDividerIndex(html, "<b>AC</b>");

		if (armorDivider is not null)
		{
			html = html.Insert(armorDivider.Value, "<div class=\"divider\"></div>");
		}

		var speedDivider = FindDividerIndex(html, "<b>Speed</b>");

		if (speedDivider is not null)
		{
			html = html.Insert(speedDivider.Value, "<div class=\"divider\"></div>");
		}

		return html;
	}

	private static int? FindDividerIndex(string html, string tag)
	{
		int hpIndex = html.IndexOf(tag);

		return hpIndex > 0 ? hpIndex : null;
	}
}

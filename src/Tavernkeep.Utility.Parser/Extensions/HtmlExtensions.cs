using AngleSharp.Dom;

namespace Tavernkeep.Utility.Parser.Extensions;

internal static class HtmlExtensions
{
	public static void AddFontAwesome(this IDocument document)
	{
		var style = document.CreateElement("link");
		style.SetAttribute("rel", "stylesheet");
		style.SetAttribute("href", "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css");

		document.Head!.AppendChild(style);
	}

	public static void AddTailwindCss(this IDocument document)
	{
		var style = document.CreateElement("link");
		style.SetAttribute("rel", "stylesheet");
		style.SetAttribute("href", "../styles/tailwind.css");

		document.Head!.AppendChild(style);
	}

	public static void AddTavernkeepCss(this IDocument document)
	{
		var style = document.CreateElement("link");
		style.SetAttribute("rel", "stylesheet");
		style.SetAttribute("href", "../styles/tavernkeep.css");

		document.Head!.AppendChild(style);
	}

	public static IElement CreateTrait(this IDocument document, string text, params string[] classes)
	{
		var trait = document.CreateElement("div");
		trait.ClassList.Add(["pf-trait", .. classes]);
		trait.TextContent = text;

		return trait;
	}
}

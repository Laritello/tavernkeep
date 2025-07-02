using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser.Html.Enrichers;

internal class HeaderEnricher : BasePathbuilderEnricher
{
	protected override void EnrichContainer(PathbuilderRecord source)
	{
		Container.ClassList.Add("flex", "flex-row");

		var name = Document.CreateElement("h1");
		name.ClassList.Add("flex-1");
		name.TextContent = source.Name;
		Container.AppendChild(name);

		var type = Document.CreateElement("h1");
		type.TextContent = $"CREATURE {source.Level}";
		Container.AppendChild(type);
	}
}

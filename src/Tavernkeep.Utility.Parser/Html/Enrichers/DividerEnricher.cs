using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser.Html.Enrichers;

internal class DividerEnricher : BasePathbuilderEnricher
{
	protected override void EnrichContainer(PathbuilderRecord source) => Container.ClassList.Add("divider");
}

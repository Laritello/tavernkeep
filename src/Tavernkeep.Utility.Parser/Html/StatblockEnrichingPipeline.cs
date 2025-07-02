using AngleSharp.Dom;
using Tavernkeep.Utility.Parser.Html.Enrichers;
using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser.Html;

internal class StatblockEnrichingPipeline(IEnumerable<IStatblockEnricher<PathbuilderRecord>> enrichers) : IStatblockEnricher<PathbuilderRecord>
{
	private readonly IEnumerable<IStatblockEnricher<PathbuilderRecord>> _enrichers = enrichers;

	public IDocument Enrich(IDocument document, PathbuilderRecord record)
	{
		foreach (var converter in _enrichers)
		{
			converter.Enrich(document, record);
		}

		return document;
	}
}

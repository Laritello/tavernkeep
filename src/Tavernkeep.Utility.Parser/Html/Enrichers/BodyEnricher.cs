using AngleSharp.Html.Parser;
using Tavernkeep.Utility.Parser.Html.Processors;
using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser.Html.Enrichers;

internal partial class BodyEnricher : BasePathbuilderEnricher
{
	private readonly HtmlParser _parser;
	private readonly HtmlProcessingPipeline _pipeline;

	public BodyEnricher()
	{
		List<IHtmlProcessor> processors =
		[
			new SanitizeProcessor(),
			new DividerProcessor(),
			new StatblockItemProcessor(),
			new ActionProcessor()
		];

		_parser = new();
		_pipeline = new HtmlProcessingPipeline(processors);
	}

	protected override void EnrichContainer(PathbuilderRecord source) => Container.AppendChild(_parser.ParseDocument(ProcessHtml(source.Statblock)).DocumentElement);

	private string ProcessHtml(string html) => _pipeline.Process(html);
}

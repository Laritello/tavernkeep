using Tavernkeep.Utility.Parser.Html.Processors;

namespace Tavernkeep.Utility.Parser.Html;

public class HtmlProcessingPipeline(IEnumerable<IHtmlProcessor> processors) : IHtmlProcessor
{
	private readonly IEnumerable<IHtmlProcessor> _processors = processors;

	public string Process(string html) => _processors.Aggregate(html, (current, processor) => processor.Process(current));
}

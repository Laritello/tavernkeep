using AngleSharp.Dom;
using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser.Html.Enrichers;

public abstract class BasePathbuilderEnricher : IStatblockEnricher<PathbuilderRecord>
{
	protected IDocument Document { get; private set; } = null!;
	protected IElement Container { get; private set; } = null!;

	public IDocument Enrich(IDocument document, PathbuilderRecord source)
	{
		Document = document;
		Container = document.CreateElement("div");

		if (document.Body!.Children.Length == 0)
		{
			InitializeContainer();
		}

		EnrichContainer(source);

		document.Body.FirstChild!.AppendChild(Container);
		return document;
	}

	private void InitializeContainer()
	{
		var container = Document.CreateElement("div");
		container.ClassList.Add("statblock");
		Document.Body!.AppendChild(container);
	}

	protected abstract void EnrichContainer(PathbuilderRecord source);
}

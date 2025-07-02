using AngleSharp.Dom;

namespace Tavernkeep.Utility.Parser.Html.Enrichers
{
	public interface IStatblockEnricher<T>
	{
		public IDocument Enrich(IDocument document, T source);
	}
}

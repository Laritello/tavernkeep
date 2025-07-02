using AngleSharp;
using Tavernkeep.Core.Entities.Library.Creatures;
using Tavernkeep.Utility.Parser.Html;
using Tavernkeep.Utility.Parser.Html.Enrichers;
using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser
{
	internal class Parser
	{
		private static readonly List<IStatblockEnricher<PathbuilderRecord>> _enrichers =
		[
			new HeaderEnricher(),
			new DividerEnricher(),
			new TraitsEnricher(),
			new BodyEnricher(),
		];

		private static readonly StatblockEnrichingPipeline _pipeline = new(_enrichers);

		public Creature Parse(PathbuilderRecord record)
		{
			return new Creature()
			{
				Name = record.Name,
				Level = int.Parse(record.Level),
				Traits = record.Traits.Split(","),
				Type = "Creature",
				Statblock = CreateStatblock(record)
			};
		}

		private string CreateStatblock(PathbuilderRecord record)
		{
			var document = BrowsingContext.New().OpenNewAsync().Result;
			return _pipeline.Enrich(document, record).Body?.InnerHtml ?? throw new Exception("Error parsing statblock.");
		}
	}
}

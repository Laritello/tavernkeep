using Tavernkeep.Utility.Parser.Extensions;
using Tavernkeep.Utility.Parser.Json;

namespace Tavernkeep.Utility.Parser.Html.Enrichers;

internal class TraitsEnricher : BasePathbuilderEnricher
{
	private static readonly List<string> Rarities = ["Uncommon", "Rare", "Unique"];

	protected override void EnrichContainer(PathbuilderRecord source)
	{
		Container.ClassList.Add("flex", "flex-row", "flex-wrap", "max-w-full");

		var traits = source.Traits.Split(",").Select(x => x.Trim()).ToList();

		AddRarity(traits);
		AddAlignment(source.Alignment);
		AddSize(source.Size);

		traits.ForEach(trait => Container.AppendChild(Document.CreateTrait(trait)));
	}

	private void AddRarity(List<string> traits)
	{
		var rarity = traits.FirstOrDefault(Rarities.Contains);

		if (rarity is null)
		{
			return;
		}

		traits.Remove(rarity);
		Container.AppendChild(Document.CreateTrait(rarity, $"pf-trait-{rarity.ToLower()}"));
	}

	private void AddAlignment(string alignment)
	{
		if (string.IsNullOrEmpty(alignment))
		{
			return;
		}

		Container.AppendChild(Document.CreateTrait(alignment, "pf-trait-align"));
	}

	private void AddSize(string size)
	{
		if (string.IsNullOrEmpty(size))
		{
			return;
		}

		Container.AppendChild(Document.CreateTrait(size, "pf-trait-size"));
	}
}

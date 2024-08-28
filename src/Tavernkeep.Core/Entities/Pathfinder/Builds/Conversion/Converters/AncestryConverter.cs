using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters
{
	public class AncestryConverter(Character character, AncestryTemplate ancestryTemplate) : IBuildConverter<Ancestry>
	{
		public Ancestry Convert()
		{
			var snapshot = character.Snapshot.Ancestry;
			var template = ancestryTemplate;

			if (snapshot.Id != template.Id)
				throw new ArgumentException();

			Ancestry convertedAncestry = new()
			{
				Id = template.Id,
				Name = template.Name,
			};

			foreach (var attribute in template.Attributes)
			{
				convertedAncestry.Attributes.Add(attribute.Convert(snapshot));
			}

			return convertedAncestry;
		}
	}
}

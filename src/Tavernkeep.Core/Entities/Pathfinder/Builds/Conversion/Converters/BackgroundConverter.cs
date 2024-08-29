using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters
{
	public class BackgroundConverter(Character character, BackgroundTemplate backgroundTemplate) : IBuildConverter<Background>
	{
		public Background Convert()
		{
			var snapshot = character.Snapshot.Background;
			var template = backgroundTemplate;

			if (snapshot.Id != template.Id)
				throw new ArgumentException();

			Background convertedBackground = new()
			{
				Id = template.Id,
				Name = template.Name,
			};

			foreach (var attribute in template.Attributes)
			{
				convertedBackground.Attributes.Add(attribute.Convert(snapshot));
			}

			return convertedBackground;
		}
	}
}

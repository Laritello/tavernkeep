using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters
{
	public class GeneralConverter(Character character) : IBuildConverter<General>
	{
		// LMAO FIX THIS
		public General Convert()
		{
			var generalTemplate = new General();

			var general = new General()
			{
				Attributes = []
			};

			foreach (var attribute in generalTemplate.Attributes)
			{
				general.Attributes.Add(attribute.Convert(character.Snapshot.General));
			}

			return general;
		}
	}
}

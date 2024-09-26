using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters
{
	public class GeneralConverter(Character character, General generalTemplate) : IBuildConverter<General, General>
	{
		public General Convert()
		{
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

		public General Restore()
		{
			return character.Build.General;
		}

		public BuildSnapshot Snapshot()
		{
			BuildSnapshot snapshot = new();
			
			foreach (var attribute in generalTemplate.Attributes)
			{
				snapshot.Values.Add(attribute.Snapshot());
			}

			return snapshot;
		}
	}
}

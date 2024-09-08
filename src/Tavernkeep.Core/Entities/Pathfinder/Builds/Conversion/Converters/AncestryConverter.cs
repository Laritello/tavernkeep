using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters
{
	public class AncestryConverter(Character character, AncestryTemplate ancestryTemplate) : IBuildConverter<Ancestry, AncestryTemplate>
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
				HitPoints = template.HitPoints,
				Size = template.Size,
				Speed = template.Speed,
				Languagues = template.Languagues,
			};

			foreach (var attribute in template.Attributes)
			{
				convertedAncestry.Attributes.Add(attribute.Convert(snapshot));
			}

			return convertedAncestry;
		}

		public AncestryTemplate Restore()
		{
			var snapshot = character.Snapshot.Ancestry;
			var template = ancestryTemplate;

			if (snapshot.Id != template.Id)
				throw new ArgumentException();

			foreach (var attribute in template.Attributes)
			{
				attribute.Restore(snapshot);
			}

			return template;
		}

		public BuildSnapshot Snapshot()
		{
			BuildSnapshot snapshot = new()
			{
				Id = ancestryTemplate.Id
			};

			foreach (var attribute in ancestryTemplate.Attributes)
			{
				snapshot.Values.Add(attribute.Snapshot());
			}

			return snapshot;
		}
	}
}

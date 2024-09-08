using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Evaluators.Builds;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion.Converters
{
	public class ClassConverter(Character character, AncestryTemplate ancestryTemplate, ClassTemplate classTemplate) : IBuildConverter<Class, ClassTemplate>
	{
		public Class Convert()
		{
			var snapshot = character.Snapshot.Class;
			var template = classTemplate;

			if (snapshot.Id != template.Id)
				throw new ArgumentException();

			Class convertedClass = new()
			{
				Id = template.Id,
				Name = template.Name,
				HitPoints = template.HitPoints,
			};

			ConversionParameters parameters = new()
			{
				{ "IntelligenceModifier", GetIntelligenceModifier() }
			};

			foreach (var attribute in template.Attributes)
			{
				convertedClass.Attributes.Add(attribute.Convert(snapshot, parameters));
			}

			return convertedClass;
		}

		public ClassTemplate Restore()
		{
			var snapshot = character.Snapshot.Class;
			var template = classTemplate;

			if (snapshot.Id != template.Id)
				throw new ArgumentException();

			ConversionParameters parameters = new()
			{
				{ "IntelligenceModifier", GetIntelligenceModifier() }
			};

			foreach (var attribute in template.Attributes)
			{
				attribute.Restore(snapshot, parameters);
			}

			return template;
		}

		private int GetIntelligenceModifier()
		{
			List<AbilityModifierAttribute> template =
			[
				.. character.Build.General.Attributes.Where(x => x.Level <= character.Build.Level && x is AbilityModifierAttribute).Cast<AbilityModifierAttribute>(),
				.. ancestryTemplate.Attributes.Where(x => x.Level <= character.Build.Level && x is AbilityModifierAttribute).Cast<AbilityModifierAttribute>(),
				.. classTemplate.Attributes.Where(x => x.Level <= character.Build.Level && x is AbilityModifierAttribute).Cast<AbilityModifierAttribute>(),
			];

			List<BuildValue> snapshot =
			[
				.. character.Snapshot.General.Values,
				.. character.Snapshot.Ancestry.Values,
				.. character.Snapshot.Class.Values,
			];

			var evaluator = new IntelligenceModifierEvaluator(template, snapshot);

			return evaluator.Value;
		}

		public BuildSnapshot Snapshot()
		{
			BuildSnapshot snapshot = new()
			{
				Id = classTemplate.Id
			};

			foreach (var attribute in classTemplate.Attributes)
			{
				snapshot.Values.Add(attribute.Snapshot());
			}

			return snapshot;
		}
	}
}

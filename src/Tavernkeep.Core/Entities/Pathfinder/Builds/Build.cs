using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Build
	{
		[NotMapped]
		private Dictionary<int, LevelProgression>? _progression;

		[JsonIgnore]
		public Character Owner { get; init; }
		public int Level { get; set; } = 1;

		/// <summary>
		/// Character's general build.
		/// <para>Contains level progression that is common for all characters.</para>
		/// </summary>
		public General General { get; set; } = General.Template;

		/// <summary>
		/// Character's ancestry build.
		/// <para>Contains level progression that is specified by the selected Ancestry.</para>
		/// </summary>
		public Ancestry Ancestry { get; set; } = Ancestry.Empty;

		/// <summary>
		/// Character's background build.
		/// <para>Contains level progression that is specified by the selected Background.</para>
		/// </summary>
		public Background Background { get; set; } = Background.Empty;

		/// <summary>
		/// Character's class build.
		/// <para>Contains level progression that is specified by the selected Class.</para>
		/// </summary>
		public Class Class { get; set; } = Class.Empty;

		[NotMapped]
		public Dictionary<int, LevelProgression> Progression => _progression ??= CollectProgression();

		public Build()
		{

		}

		public Build(Character owner)
		{
			Owner = owner;
		}

		private Dictionary<int, LevelProgression> CollectProgression()
		{
			Dictionary<int, LevelProgression> result = [];

			for (int level = 1; level <= 20; level++)
			{
				result.Add(level, General[level] + Ancestry[level] + Background[level] + Class[level]);
			}

			return result;
		}
	}
}

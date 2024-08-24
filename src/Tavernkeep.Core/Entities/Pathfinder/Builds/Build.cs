using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Build
	{
		[NotMapped]
		private List<Advancement>? _advancements;

		[JsonIgnore]
		public Character Owner { get; init; }
		public int Level { get; set; } = 1;

		/// <summary>
		/// Character's general build.
		/// <para>Contains level progression that is common for all characters.</para>
		/// </summary>
		public General General { get; set; } = new();

		/// <summary>
		/// Character's ancestry build.
		/// <para>Contains level progression that is specified by the selected Ancestry.</para>
		/// </summary>
		public Ancestry Ancestry { get; set; } = new();

		/// <summary>
		/// Character's background build.
		/// <para>Contains level progression that is specified by the selected Background.</para>
		/// </summary>
		public Background Background { get; set; } = new();

		/// <summary>
		/// Character's class build.
		/// <para>Contains level progression that is specified by the selected Class.</para>
		/// </summary>
		public Class Class { get; set; } = new();

		[NotMapped]
		public List<Advancement> Advancements => _advancements ??= CollectAdvancements();

		public Build()
		{

		}

		public Build(Character owner)
		{
			Owner = owner;
		}

		private List<Advancement> CollectAdvancements()
		{
			var advancements = General.Advancements.Concat(Ancestry.Advancements).Concat(Background.Advancements).Concat(Class.Advancements);
			return [.. advancements.OrderBy(x => x.Level)];
		}
	}
}

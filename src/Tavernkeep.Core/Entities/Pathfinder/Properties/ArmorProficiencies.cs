using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class ArmorProficiencies
	{
		public Proficiency Unarmored { get; set; }
		public Proficiency Light { get; set; }
		public Proficiency Medium { get; set; }
		public Proficiency Heavy { get; set; }

		public ArmorProficiencies()
		{
			Unarmored = Proficiency.Untrained;
			Light = Proficiency.Untrained;
			Medium = Proficiency.Untrained;
			Heavy = Proficiency.Untrained;
		}

		public ArmorProficiencies(
			Proficiency unarmored = Proficiency.Untrained,
			Proficiency light = Proficiency.Untrained,
			Proficiency medium = Proficiency.Untrained,
			Proficiency heavy = Proficiency.Untrained
			)
		{
			Unarmored = unarmored;
			Light = light;
			Medium = medium;
			Heavy = heavy;
		}

		public Proficiency this[ArmorType type]
		{
			get
			{
				return type switch
				{
					ArmorType.Unarmored => Unarmored,
					ArmorType.Light => Light,
					ArmorType.Medium => Medium,
					ArmorType.Heavy => Heavy,
					_ => throw new NotImplementedException(),
				};
			}

			set
			{
				switch (type)
				{
					case ArmorType.Unarmored:
						Unarmored = value;
						break;
					case ArmorType.Light:
						Light = value;
						break;
					case ArmorType.Medium:
						Medium = value;
						break;
					case ArmorType.Heavy:
						Heavy = value;
						break;
				}
			}
		}
	}
}

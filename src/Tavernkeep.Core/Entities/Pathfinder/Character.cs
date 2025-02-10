using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.DataStructures;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder
{
	[Table("Character")]
	public class Character : GuidEntity
	{
		#region Constructors

		public Character()
		{
			Abilities = [];
			Skills = [];

			Walk = new(this, SpeedType.Walk, true);
			Burrow = new(this, SpeedType.Burrow);
			Climb = new(this, SpeedType.Climb);
			Fly = new(this, SpeedType.Fly);
			Swim = new(this, SpeedType.Swim);

			Armor = new(this);

			HeroPoints = 1;

			Conditions = [];
		}

		#endregion

		#region Properties
		public User Owner { get; set; } = default!;

		public Portrait? Portrait { get; set; }

		public string Name { get; set; } = default!;

		public Class Class { get; set; } = default!;
		public Ancestry Ancestry { get; set; } = default!;

		public int HeroPoints { get; set; }

		public int Level { get; set; }
		public Health Health { get; set; } = default!;
		public Armor Armor { get; set; }
		public List<Condition> Conditions { get; set; }

		public EntityCollection<Ability> Abilities { get; set; }
		public EntityCollection<Skill> Skills { get; set; }

		public Speed Walk { get; set; }
		public Speed Burrow { get; set; }
		public Speed Climb { get; set; }
		public Speed Fly { get; set; }
		public Speed Swim { get; set; }

		#endregion

		#region Methods

		public Speed GetSpeed(SpeedType type)
		{
			return type switch
			{
				SpeedType.Walk => Walk,
				SpeedType.Burrow => Burrow,
				SpeedType.Climb => Climb,
				SpeedType.Fly => Fly,
				SpeedType.Swim => Swim,
				_ => throw new NotImplementedException(),
			};
		}

		public void AddCondition(Condition condition)
		{
			Conditions.Add(condition);
		}

		#endregion
	}
}

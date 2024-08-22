using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Calculations.Managers;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Skill
	{
		#region Backing fields

		private IPropertyManager _manager;

		#endregion

		#region Constructors

		public Skill()
		{

		}

		public Skill(Character owner, AbilityType baseAbility, SkillType type)
		{
			Owner = owner;
			BaseAbility = baseAbility;
			Type = type;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;

		[JsonIgnore]
		[NotMapped]
		public IPropertyManager Manager => _manager ??= new SkillPropertyManager(this);
		public AbilityType BaseAbility { get; set; }
		public SkillType Type { get; set; }
		public Proficiency Proficiency { get; set; }
		public int Bonus => Manager.Value;

		#endregion

		#region Methods

		public SkillSnapshot AsSnapshot()
		{
			return new()
			{
				Type = Type,
				Proficiency = Proficiency,
				Bonus = Bonus
			};
		}

		#endregion
	}
}

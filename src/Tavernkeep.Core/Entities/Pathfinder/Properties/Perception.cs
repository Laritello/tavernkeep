using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers.Managers;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Perception : IModifiable
	{
		#region Backing fields

		private IModifierManager _manager;

		#endregion

		#region Constructors

		public Perception()
		{

		}

		public Perception(Character owner)
		{
			Owner = owner;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;

		[JsonIgnore]
		[NotMapped]
		public IModifierManager Manager => _manager ??= new GeneralModifierManager(Owner, ModifierTarget.Perception);
		public Proficiency Proficiency { get; set; }
		public int Bonus => Owner.Wisdom.Modifier + Proficiency.GetProficiencyBonus(Owner) + Manager.GetSummary().Total;

		#endregion

		#region Methods

		public PerceptionSnapshot AsSnapshot()
		{
			return new()
			{
				Proficiency = Proficiency,
				Bonus = Bonus
			};
		}

		#endregion
	}
}

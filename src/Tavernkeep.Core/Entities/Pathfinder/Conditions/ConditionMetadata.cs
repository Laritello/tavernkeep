﻿using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	[Table("Conditions")]
	public class ConditionMetadata : NameEntity
	{
		#region Constructors

		public ConditionMetadata() { }

		#endregion

		#region Properties

		public string Description { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int Level { get; set; }

		public List<Condition> Related { get; set; } = [];
		public List<Modifier> Modifiers { get; set; } = [];

		#endregion

		#region Methods

		public Condition ToCondition()
		{
			return new Condition()
			{
				Name = Name,
				HasLevels = HasLevels,
				Level = Level,
				Related = Related,
				Modifiers = Modifiers
			};
		}

		#endregion
	}
}

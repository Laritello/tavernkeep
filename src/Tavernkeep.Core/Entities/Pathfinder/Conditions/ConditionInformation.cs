using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	[Table("Conditions")]
	public class ConditionInformation : StringEntity
	{
		#region Constructors

		public ConditionInformation() { }

		#endregion

		#region Properties
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int Level { get; set; }

		public List<Condition> Related { get; set; } = [];
		public List<Modifier> Modifiers { get; set; } = [];

		#endregion

		#region Methods

		public Condition ToCondition(int level = 1)
		{
			return new Condition()
			{
				Name = Name,
				HasLevels = HasLevels,
				Level = level,
				Related = Related,
				Modifiers = Modifiers.Select(x => x.Copy()).ToList() // EF Tracking issue temporary fix
			};
		}

		#endregion
	}
}

using NCalc;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Pathfinder.Conditions
{
	[Table("ConditionRecord")]
	public abstract class ConditionRecord : GuidEntity
	{
		#region Constructors

		public ConditionRecord() { }

		#endregion

		#region Properties

		public required Condition Condition { get; set; }
		public int? Level { get; set; }

		#endregion

		#region Methods

		public abstract int this[string targetName] { get; }

		public bool HasModifier(string targetName, ModifierType type) =>
			Condition.Modifiers.ContainsKey(targetName) && Condition.Modifiers[targetName].Type == type;

		#endregion
	}
}

namespace Tavernkeep.Core.Contracts.Enums
{
	/// <summary>
	/// Describes the scaling of a modifier.
	/// </summary>
	public enum ModifierScaling
	{
		/// <summary>
		/// Modifier value doesn't change.
		/// </summary>
		Constant,

		/// <summary>
		/// Modifier value changes based on condition level.
		/// </summary>
		ConditionLeveled,

		/// <summary>
		/// Modifier value changes based on character level.
		/// </summary>
		CharacterLeveled
	}
}

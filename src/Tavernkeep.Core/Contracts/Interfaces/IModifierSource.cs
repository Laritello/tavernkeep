using Tavernkeep.Core.Entities.Pathfinder.Modifiers;

namespace Tavernkeep.Core.Contracts.Interfaces
{
	public interface IModifierSource
	{
		public string Name { get; }
		public List<Modifier> Modifiers { get; }
	}
}

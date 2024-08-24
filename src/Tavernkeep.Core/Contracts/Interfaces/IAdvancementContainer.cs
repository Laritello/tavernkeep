using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Contracts.Interfaces
{
	public interface IAdvancementContainer
	{
		public ICollection<Advancement> Advancements { get; }
	}
}

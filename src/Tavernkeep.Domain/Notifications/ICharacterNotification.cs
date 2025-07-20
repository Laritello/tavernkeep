using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Domain.Notifications
{
	public interface ICharacterNotification : IBaseNotification
	{
		public Character Character { get; }
	}
}

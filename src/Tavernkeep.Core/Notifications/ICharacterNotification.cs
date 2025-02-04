using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Notifications
{
	public interface ICharacterNotification : IBaseNotification
	{
		public Character Character { get; }
	}
}

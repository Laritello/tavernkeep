using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Core.Notifications
{
	public interface IEncounterNotification : IBaseNotification
	{
		public Encounter Encounter { get; set; }
	}
}

using Tavernkeep.Domain.Entities.Encounters;

namespace Tavernkeep.Domain.Notifications;

public interface IEncounterNotification : IBaseNotification
{
	public Encounter Encounter { get; set; }
}

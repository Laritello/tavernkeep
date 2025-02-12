using MediatR;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterUpdated
{
	public class EncounterUpdatedNotification(Encounter encounter) : INotification, IEncounterNotification
	{
		public Encounter Encounter { get; set; } = encounter;
	}
}

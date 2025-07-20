using MediatR;
using Tavernkeep.Domain.Entities.Encounters;
using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterUpdated
{
	public class EncounterUpdatedNotification(Encounter encounter) : INotification, IEncounterNotification
	{
		public Encounter Encounter { get; set; } = encounter;
	}
}

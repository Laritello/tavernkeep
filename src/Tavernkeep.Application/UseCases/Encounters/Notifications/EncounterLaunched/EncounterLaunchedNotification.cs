using MediatR;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Encounters.Notifications.EncounterLaunched
{
	public class EncounterLaunchedNotification(Encounter encounter) : INotification, IEncounterNotification
	{
		public Encounter Encounter { get; set; } = encounter;
	}
}

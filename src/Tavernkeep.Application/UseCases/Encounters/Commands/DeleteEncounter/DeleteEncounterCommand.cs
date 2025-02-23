using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteEncounter
{
	public class DeleteEncounterCommand(Guid encounterId) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
	}
}

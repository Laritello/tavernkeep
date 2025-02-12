using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditEncounterStatus
{
	public class EditEncounterStatusCommand(Guid encounterId, EncounterStatus status) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
		public EncounterStatus Status { get; set; } = status;
	}
}

using MediatR;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteEncounterParticipant
{
	public class DeleteEncounterParticipantCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<DeleteEncounterParticipantCommand>
	{
		public async Task Handle(DeleteEncounterParticipantCommand request, CancellationToken cancellationToken)
		{
			await encounterService.DeleteParticipantAsync(request.EncounterId, request.ParticipantId, cancellationToken);
		}
	}
}

using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.RemoveEncounterParticipant
{
	public class RemoveEncounterParticipantCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<RemoveEncounterParticipantCommand>
	{
		public async Task Handle(RemoveEncounterParticipantCommand request, CancellationToken cancellationToken)
		{
			await encounterService.RemoveParticipantAsync(request.EncounterId, request.ParticipantId, cancellationToken);
		}
	}
}

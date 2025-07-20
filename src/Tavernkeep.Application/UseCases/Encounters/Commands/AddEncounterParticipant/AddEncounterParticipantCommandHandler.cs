using MediatR;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.AddEncounterParticipant
{
	public class AddEncounterParticipantCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<AddEncounterParticipantCommand>
	{
		public async Task Handle(AddEncounterParticipantCommand request, CancellationToken cancellationToken)
		{
			await encounterService.AddParticipantAsync(request.EncounterId, request.Type, request.ParticipantId, cancellationToken);
		}
	}
}

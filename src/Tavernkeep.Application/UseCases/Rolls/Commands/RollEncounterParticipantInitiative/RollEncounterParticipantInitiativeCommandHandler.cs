using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Rolls.Commands.RollEncounterParticipantInitiative
{
	public class RollEncounterParticipantInitiativeCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<RollEncounterParticipantInitiativeCommand>
	{
		public async Task Handle(RollEncounterParticipantInitiativeCommand request, CancellationToken cancellationToken)
		{
			await encounterService.RollInitiativeForParticipantAsync(request.EncounterId, request.InitiatorId, request.ParticipantId, request.InitiativeSkill, cancellationToken);
		}
	}
}

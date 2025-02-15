using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.SetParticipantInitiative
{
	public class SetParticipantInitiativeCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<SetParticipantInitiativeCommand>
	{
		public async Task Handle(SetParticipantInitiativeCommand request, CancellationToken cancellationToken)
		{
			await encounterService.SetInitiativeForParticipantAsync(request.EncounterId, request.InitiatorId, request.ParticipantId, request.Result, cancellationToken);
		}
	}
}

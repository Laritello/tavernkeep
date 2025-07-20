using MediatR;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Rolls.Commands.RollEncounterInitiative
{
	public class RollEncounterInitiativeCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<RollEncounterInitiativeCommand>
	{
		public async Task Handle(RollEncounterInitiativeCommand request, CancellationToken cancellationToken)
		{
			await encounterService.RollInitiativeAsync(request.EncounterId, request.InitiatorId, request.NPCOnly, cancellationToken);
		}
	}
}

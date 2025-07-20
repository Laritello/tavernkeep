using MediatR;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.AddConditionToParticipant;

public class AddConditionToParticipantCommandHandler(IEncounterService encounterService) : IRequestHandler<AddConditionToParticipantCommand>
{
	public async Task Handle(AddConditionToParticipantCommand request, CancellationToken cancellationToken)
	{
		await encounterService.AddConditionToParticipantAsync(request.EncounterId, request.ParticipantId, request.Name, cancellationToken);
	}
}

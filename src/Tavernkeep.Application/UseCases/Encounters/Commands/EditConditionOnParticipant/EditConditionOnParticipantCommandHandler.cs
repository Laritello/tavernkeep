using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditConditionOnParticipant
{
	public class EditConditionOnParticipantCommandHandler(IEncounterService encounterService) : IRequestHandler<EditConditionOnParticipantCommand>
	{
		public async Task Handle(EditConditionOnParticipantCommand request, CancellationToken cancellationToken)
		{
			await encounterService.EditConditionOnParticipantAsync(request.EncounterId, request.ParticipantId, request.Name, request.Level, cancellationToken);
		}
	}
}

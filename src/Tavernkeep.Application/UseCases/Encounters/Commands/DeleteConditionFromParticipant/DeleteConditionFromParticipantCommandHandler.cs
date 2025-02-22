using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteConditionFromParticipant
{
	public class DeleteConditionFromParticipantCommandHandler(IEncounterService encounterService) : IRequestHandler<DeleteConditionFromParticipantCommand>
	{
		public async Task Handle(DeleteConditionFromParticipantCommand request, CancellationToken cancellationToken)
		{
			await encounterService.DeleteConditionFormParticipantAsync(request.EncounterId, request.ParticipantId, request.Name, cancellationToken);
		}
	}
}

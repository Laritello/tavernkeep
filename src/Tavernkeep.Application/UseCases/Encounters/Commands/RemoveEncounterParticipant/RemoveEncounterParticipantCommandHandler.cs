using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.RemoveEncounterParticipant
{
	public class RemoveEncounterParticipantCommandHandler : IRequestHandler<RemoveEncounterParticipantCommand>
	{
		public Task Handle(RemoveEncounterParticipantCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

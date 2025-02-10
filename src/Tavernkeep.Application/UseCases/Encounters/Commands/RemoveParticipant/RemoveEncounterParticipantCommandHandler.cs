using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.RemoveParticipant
{
	public class RemoveEncounterParticipantCommandHandler : IRequestHandler<RemoveEncounterParticipantCommand>
	{
		public Task Handle(RemoveEncounterParticipantCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

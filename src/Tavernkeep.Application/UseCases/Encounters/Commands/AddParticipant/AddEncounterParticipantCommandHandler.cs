using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.AddParticipant
{
	public class AddEncounterParticipantCommandHandler : IRequestHandler<AddEncounterParticipantCommand>
	{
		public Task Handle(AddEncounterParticipantCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

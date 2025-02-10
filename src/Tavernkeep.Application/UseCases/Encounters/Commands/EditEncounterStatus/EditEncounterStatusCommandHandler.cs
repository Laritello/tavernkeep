using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditEncounterStatus
{
	public class EditEncounterStatusCommandHandler : IRequestHandler<EditEncounterStatusCommand>
	{
		public Task Handle(EditEncounterStatusCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

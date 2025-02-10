using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteEncounter
{
	public class DeleteEncounterCommandHandler : IRequestHandler<DeleteEncounterCommand>
	{
		public Task Handle(DeleteEncounterCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

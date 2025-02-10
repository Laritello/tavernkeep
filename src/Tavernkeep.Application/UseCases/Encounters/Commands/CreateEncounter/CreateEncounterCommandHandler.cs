using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter
{
	public class CreateEncounterCommandHandler : IRequestHandler<CreateEncounterCommand>
	{
		public Task Handle(CreateEncounterCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

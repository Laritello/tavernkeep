using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.DeleteEncounter
{
	public class DeleteEncounterCommandHandler(IEncounterService encounterService) : IRequestHandler<DeleteEncounterCommand>
	{
		public async Task Handle(DeleteEncounterCommand request, CancellationToken cancellationToken)
		{
			await encounterService.DeleteEncounterAsync(request.EncounterId, cancellationToken);
		}
	}
}

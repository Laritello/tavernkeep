using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditEncounterStatus
{
	public class EditEncounterStatusCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<EditEncounterStatusCommand>
	{
		public async Task Handle(EditEncounterStatusCommand request, CancellationToken cancellationToken)
		{
			await encounterService.UpdateEncounterStatusAsync(request.EncounterId, request.Status, cancellationToken);
		}
	}
}

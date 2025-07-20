using MediatR;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditEncounterStatus;

public class EditEncounterStatusCommandHandler(
	IEncounterService encounterService
	) : IRequestHandler<EditEncounterStatusCommand>
{
	public async Task Handle(EditEncounterStatusCommand request, CancellationToken cancellationToken)
	{
		await encounterService.EditEncounterStatusAsync(request.EncounterId, request.Status, cancellationToken);
	}
}

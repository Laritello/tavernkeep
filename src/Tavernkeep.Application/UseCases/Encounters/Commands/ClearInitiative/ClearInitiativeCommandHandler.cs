using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.ClearInitiative
{
	public class ClearInitiativeCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<ClearInitiativeCommand>
	{
		public async Task Handle(ClearInitiativeCommand request, CancellationToken cancellationToken)
		{
			await encounterService.ClearInitiativeAsync(request.EncounterId, cancellationToken);
		}
	}
}

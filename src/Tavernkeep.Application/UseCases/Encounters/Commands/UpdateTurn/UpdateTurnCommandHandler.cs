using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.UpdateTurn
{
	public class UpdateTurnCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<UpdateTurnCommand>
	{
		public async Task Handle(UpdateTurnCommand request, CancellationToken cancellationToken)
		{
			await encounterService.UpdateTurnAsync(request.EncounterId, request.MoveForward, cancellationToken);
		}
	}
}

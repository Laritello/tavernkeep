using MediatR;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.UpdateParticipantsOrdinal
{
	public class UpdateParticipantsOrdinalCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<UpdateParticipantsOrdinalCommand>
	{
		public async Task Handle(UpdateParticipantsOrdinalCommand request, CancellationToken cancellationToken)
		{
			await encounterService.UpdateParticipantsOrdinalAsync(request.EncounterId, request.Ordinals, cancellationToken);
		}
	}
}

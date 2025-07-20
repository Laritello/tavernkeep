using MediatR;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditParticipantsOrdinal
{
	public class EditParticipantsOrdinalCommandHandler(
		IEncounterService encounterService
		) : IRequestHandler<EditParticipantsOrdinalCommand>
	{
		public async Task Handle(EditParticipantsOrdinalCommand request, CancellationToken cancellationToken)
		{
			await encounterService.EditParticipantsOrdinalAsync(request.EncounterId, request.Ordinals, cancellationToken);
		}
	}
}

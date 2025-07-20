using MediatR;
using Tavernkeep.Domain.Entities.Encounters;
using Tavernkeep.Domain.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter;

public class CreateEncounterCommandHandler(IEncounterService encounterService) : IRequestHandler<CreateEncounterCommand, Encounter>
{
	public async Task<Encounter> Handle(CreateEncounterCommand request, CancellationToken cancellationToken)
	{
		return await encounterService.CreateEncounterAsync(request.Name, cancellationToken);
	}
}

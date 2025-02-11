using MediatR;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Services;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter
{
	public class CreateEncounterCommandHandler(IEncounterService encounterService) : IRequestHandler<CreateEncounterCommand, Encounter>
	{
		public async Task<Encounter> Handle(CreateEncounterCommand request, CancellationToken cancellationToken)
		{
			return await encounterService.CreateEncounterAsync(request.Name, cancellationToken);
		}
	}
}

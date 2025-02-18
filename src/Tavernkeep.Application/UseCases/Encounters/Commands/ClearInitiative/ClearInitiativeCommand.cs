using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.ClearInitiative
{
	public class ClearInitiativeCommand(Guid encounterId) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
	}
}

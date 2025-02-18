using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.UpdateTurn
{
	public class UpdateTurnCommand(Guid encounterId, bool moveForward) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
		public bool MoveForward { get; set; } = moveForward;
	}
}

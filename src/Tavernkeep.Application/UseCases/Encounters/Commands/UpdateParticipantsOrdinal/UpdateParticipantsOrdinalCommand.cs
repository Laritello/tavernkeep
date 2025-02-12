using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.UpdateParticipantsOrdinal
{
	public class UpdateParticipantsOrdinalCommand(Guid encounterId, IList<Guid> ordinals) : IRequest
	{
		public Guid EncounterId { get; set; } = encounterId;
		public IList<Guid> Ordinals { get; set; } = ordinals;
	}
}

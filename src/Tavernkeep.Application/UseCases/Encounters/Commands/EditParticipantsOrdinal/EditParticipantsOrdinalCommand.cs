using MediatR;

namespace Tavernkeep.Application.UseCases.Encounters.Commands.EditParticipantsOrdinal;

public class EditParticipantsOrdinalCommand(Guid encounterId, IList<Guid> ordinals) : IRequest
{
	public Guid EncounterId { get; set; } = encounterId;
	public IList<Guid> Ordinals { get; set; } = ordinals;
}

using MediatR;

namespace Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore
{
	public class DeleteLoreCommand(Guid initiatorId, Guid characterId, string topic) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string Topic { get; set; } = topic;
	}
}

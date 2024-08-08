using MediatR;

namespace Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore
{
	public class DeleteLoreCommand : IRequest
	{
		public Guid InitiatorId { get; set; }
		public Guid CharacterId { get; set; }
		public string Topic { get; set; }

		public DeleteLoreCommand(Guid initiatorId, Guid characterId, string topic)
		{
			InitiatorId = initiatorId;
			CharacterId = characterId;
			Topic = topic;
		}
	}
}

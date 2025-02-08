using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tavernkeep.Application.UseCases.Portraits.Commands.UpdatePortrait
{
	public class UpdatePortraitCommand(Guid initiatorId, Guid characterId, IFormFile file) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public IFormFile File { get; set; } = file;
	}
}

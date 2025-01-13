using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditInformation
{
	public class EditInformationCommand(Guid initiatorId, Guid characterId, CharacterInformationEditDto information) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public CharacterInformationEditDto Information { get; set; } = information;
	}
}

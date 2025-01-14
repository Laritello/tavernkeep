using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest
{
	public class PerformLongRestCommand(Guid initiatorId, Guid characterId, bool restWithoutComfort, bool sleepInArmor) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public bool RestWithoutComfort { get; set; } = restWithoutComfort;
		public bool SleepInArmor { get; set; } = sleepInArmor;
	}
}

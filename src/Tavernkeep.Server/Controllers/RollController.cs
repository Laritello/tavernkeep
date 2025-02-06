using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice;
using Tavernkeep.Application.UseCases.Roll.Commands.RollSkill;
using Tavernkeep.Core.Contracts.Chat.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Roll;
using Tavernkeep.Server.Extensions;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	///  The <see cref="RollController"/> class handles roll operations within the application.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	/// <param name="mapper">The <see cref="IMapper"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class RollController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Roll a custom dice.
		/// </summary>
		/// <param name="expression">Dice expression written in the dice notation.</param>
		/// <param name="rollType">Type of the roll.</param>
		/// <returns><see cref="RollMessageDto"/> with the result of the roll.</returns>
		[HttpGet("custom")]
		public async Task<MessageDto> RollCustomDiceAsync([FromQuery] string expression, [FromQuery] RollType rollType)
		{
			var message = await mediator.Send(new RollCustomDiceCommand(HttpContext.GetUserId(), rollType, expression));
			return mapper.Map<MessageDto>(message);
		}

		/// <summary>
		/// Roll a skill check for the character.
		/// </summary>
		/// <param name="request">The request with roll parameters.</param>
		/// <returns><see cref="RollMessageDto"/> containing the result of the roll.</returns>
		[Authorize]
		[HttpPost("skill")]
		public async Task<MessageDto> RollSkillAsync([FromBody] RollSkillRequest request)
		{
			var message = await mediator.Send(new RollSkillCommand(HttpContext.GetUserId(), request.CharacterId, request.Name, request.RollType));
			return mapper.Map<MessageDto>(message);
		}
	}
}

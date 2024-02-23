using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice;
using Tavernkeep.Application.UseCases.Roll.Commands.RollSkill;
using Tavernkeep.Core.Contracts.Roll;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Server.Extensions;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    ///  The <see cref="RollController"/> class handles roll operations within the application.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class RollController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Roll custom dice.
        /// </summary>
        /// <param name="expression">Dice expression written in the dice notation.</param>
        /// <returns>The result of the roll.</returns>
        [HttpGet("custom")]
        public async Task<int> RollCustomDice([FromQuery] string expression)
        {
            return await mediator.Send(new RollCustomDiceCommand(expression));
        }

        /// <summary>
        /// Roll a skill check for the character.
        /// </summary>
        /// <param name="request">The request with roll parameters.</param>
        /// <returns><see cref="RollMessage"/> containing the result of the roll.</returns>
        [Authorize]
        [HttpPost("skill")]
        public async Task<RollMessage> RollSkill([FromBody] RollSkillRequest request)
        {
            return await mediator.Send(new RollSkillCommand(HttpContext.GetUserId(), request.CharacterId, request.SkillType, request.RollType));
        }
    }
}

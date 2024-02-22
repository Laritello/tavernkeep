using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice;

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
    }
}

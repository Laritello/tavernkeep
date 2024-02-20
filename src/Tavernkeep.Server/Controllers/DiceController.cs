using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Dice.Queries.RollCustomDice;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    ///  The <see cref="DiceController"/> class handles dice operations within the application.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class DiceController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Roll custom dice.
        /// </summary>
        /// <param name="expression">Dice expression written in the dice notation.</param>
        /// <returns>The result of the roll.</returns>
        [HttpGet("roll/custom")]
        public async Task<int> RollCustomDice([FromQuery] string expression)
        {
            return await mediator.Send(new RollCustomDiceQuery(expression));
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter;
using Tavernkeep.Application.Actions.Characters.Queries.GetCharacter;
using Tavernkeep.Core.Contracts.Character.Requests;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Server.Controllers
{
    /// <summary>
    /// The <see cref="CharactersController"/> class handles character operations within the application.
    /// </summary>
    /// <param name="mediator"></param>
    [ApiController]
    [Route("/api/[controller]")]
    public class CharactersController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a new character.
        /// </summary>
        /// <param name="request">The character creation request.</param>
        /// <returns>Created character.</returns>
        [Authorize]
        [HttpPost("create")]
        public async Task<Character> CreateCharacter(CreateCharacterRequest request)
        {
            return await mediator.Send(new CreateCharacterCommand(request.OwnerId, request.CharacterName));
        }

        /// <summary>
        /// Get an existing character.
        /// </summary>
        /// <param name="id">The character ID.</param>
        /// <returns>Specified character.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Character> GetCharacter([FromRoute] Guid id)
        {
            return await mediator.Send(new GetCharacterQuery(id));
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter;
using Tavernkeep.Application.Actions.Characters.Commands.DeleteCharacter;
using Tavernkeep.Application.Actions.Characters.Commands.EditAbility;
using Tavernkeep.Application.Actions.Characters.Commands.EditHealth;
using Tavernkeep.Application.Actions.Characters.Commands.EditSkill;
using Tavernkeep.Application.Actions.Characters.Commands.ModifyHealth;
using Tavernkeep.Application.Actions.Characters.Queries.GetCharacter;
using Tavernkeep.Application.Actions.Characters.Queries.GetCharacters;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Contracts.Character.Requests;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Server.Extensions;
using Tavernkeep.Server.Middleware;

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
        /// Get all characters.
        /// </summary>
        /// <returns>List containing all characters.</returns>
        [Authorize]
        [HttpGet]
        public async Task<List<Character>> GetCharacters()
        {
            return await mediator.Send(new GetAllCharactersQuery());
        }

        /// <summary>
        /// Create a new character.
        /// </summary>
        /// <param name="request">The character creation request.</param>
        /// <returns>Created character.</returns>
        [Authorize]
        [HttpPost("create")]
        public async Task<Character> CreateCharacter(CreateCharacterRequest request)
        {
            return await mediator.Send(new CreateCharacterCommand(HttpContext.GetUserId(), request.Name));
        }

        /// <summary>
        /// Delete an existing character.
        /// </summary>
        /// <param name="characterId">The character ID for deletion.</param>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpDelete("delete/{characterId}")]
        public async Task DeleteCharacter([FromRoute] Guid characterId)
        {
            await mediator.Send(new DeleteCharacterCommand(characterId));
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

        /// <summary>
        /// Change ability of the character.
        /// </summary>
        /// <param name="request">The ability edit request.</param>
        /// <returns>Changed ability.</returns>
        [Authorize]
        [HttpPatch("edit/ability")]
        public async Task<Ability> EditCharacterAbility([FromBody] EditAbilityRequest request)
        {
            return await mediator.Send(new EditAbilityCommand(HttpContext.GetUserId(), request.CharacterId, request.Type, request.Score));
        }

        /// <summary>
        /// Change skill of the character.
        /// </summary>
        /// <param name="request">The skill edit request.</param>
        /// <returns>Changed skill.</returns>
        [Authorize]
        [HttpPatch("edit/skill")]
        public async Task<Skill> EditCharacterSkill([FromBody] EditSkillRequest request)
        {
            return await mediator.Send(new EditSkillCommand(HttpContext.GetUserId(), request.CharacterId, request.Type, request.Proficiency));
        }

        /// <summary>
        /// Change health of the character.
        /// </summary>
        /// <param name="request">The health edit request.</param>
        /// <returns>Changed health.</returns>
        [Authorize]
        [HttpPatch("edit/health")]
        public async Task<Health> EditCharacterHealth([FromBody] EditHealthRequest request)
        {
            return await mediator.Send(new EditHealthCommand(HttpContext.GetUserId(), request.CharacterId, request.Current, request.Max, request.Temporary));
        }

        /// <summary>
        /// Apply heal or damage to the character.
        /// </summary>
        /// <param name="request">The health modify request.</param>
        /// <returns>Modified health.</returns>
        [Authorize]
        [HttpPatch("modify/health")]
        public async Task<Health> ModifyCharacterHealth([FromBody] ModifyHealthRequest request)
        {
            return await mediator.Send(new ModifyHealthCommand(HttpContext.GetUserId(), request.CharacterId, request.Change));
        }
    }
}

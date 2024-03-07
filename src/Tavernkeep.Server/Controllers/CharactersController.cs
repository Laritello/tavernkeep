using AutoMapper;
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
using Tavernkeep.Application.UseCases.Characters.Commands.AssignUser;
using Tavernkeep.Core.Contracts.Character.Dtos;
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
    /// <param name="mediator">The <see cref="IMediator"/> instance.</param>
    /// <param name="mapper">The <see cref="IMapper"/> instance.</param>
    [ApiController]
    [Route("/api/[controller]")]
    public class CharactersController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// Get all characters.
        /// </summary>
        /// <returns>List containing all characters.</returns>
        [Authorize]
        [HttpGet]
        public async Task<List<CharacterDto>> GetCharactersAsync()
        {
            var characters = await mediator.Send(new GetAllCharactersQuery());
            return mapper.Map<List<CharacterDto>>(characters);
        }

        /// <summary>
        /// Create a new character.
        /// </summary>
        /// <param name="request">The character creation request.</param>
        /// <returns>Created character.</returns>
        [Authorize]
        [HttpPost("create")]
        public async Task<CharacterDto> CreateCharacterAsync(CreateCharacterRequest request)
        {
            var character = await mediator.Send(new CreateCharacterCommand(request.OwnerId, request.Name));
            return mapper.Map<CharacterDto>(character);
        }

        /// <summary>
        /// Delete an existing character.
        /// </summary>
        /// <param name="characterId">The character ID for deletion.</param>
        [Authorize]
        [HttpDelete("delete/{characterId}")]
        public async Task DeleteCharacterAsync([FromRoute] Guid characterId)
        {
            await mediator.Send(new DeleteCharacterCommand(HttpContext.GetUserId(),characterId));
        }

        /// <summary>
        /// Assign user to the character.
        /// </summary>
        /// <param name="request">Request for assigning user to the character.</param>
        /// <returns>Updated character.</returns>
        [Authorize]
        [RequiresRole(UserRole.Master)]
        [HttpPatch("assign")]
        public async Task<CharacterDto> AssignUserToCharacterAsync([FromBody] AssignUserRequest request)
        {
            var character = await mediator.Send(new AssignUserCommand(request.CharacterId, request.UserId));
            return mapper.Map<CharacterDto>(character);
        }

        /// <summary>
        /// Get an existing character.
        /// </summary>
        /// <param name="id">The character ID.</param>
        /// <returns>Specified character.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<CharacterDto> GetCharacterAsync([FromRoute] Guid id)
        {
            var character = await mediator.Send(new GetCharacterQuery(id));
            return mapper.Map<CharacterDto>(character);
        }

        /// <summary>
        /// Change ability of the character.
        /// </summary>
        /// <param name="request">The ability edit request.</param>
        /// <returns>Changed ability.</returns>
        [Authorize]
        [HttpPatch("edit/ability")]
        public async Task<Ability> EditCharacterAbilityAsync([FromBody] EditAbilityRequest request)
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
        public async Task<Skill> EditCharacterSkillAsync([FromBody] EditSkillRequest request)
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
        public async Task<Health> EditCharacterHealthAsync([FromBody] EditHealthRequest request)
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
        public async Task<Health> ModifyCharacterHealthAsync([FromBody] ModifyHealthRequest request)
        {
            return await mediator.Send(new ModifyHealthCommand(HttpContext.GetUserId(), request.CharacterId, request.Change));
        }
    }
}

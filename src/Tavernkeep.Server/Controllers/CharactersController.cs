using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Characters.Commands.AssignUser;
using Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter;
using Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter;
using Tavernkeep.Application.UseCases.Characters.Commands.EditAbility;
using Tavernkeep.Application.UseCases.Characters.Commands.EditArmor;
using Tavernkeep.Application.UseCases.Characters.Commands.EditHealth;
using Tavernkeep.Application.UseCases.Characters.Commands.EditPerception;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrow;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSkill;
using Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Character.Requests;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
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
		public async Task<Dictionary<Guid, CharacterDto>> GetCharactersAsync()
		{
			var characters = await mediator.Send(new GetAllCharactersQuery());
			return mapper.Map<Dictionary<Guid, CharacterDto>>(characters);
		}

		/// <summary>
		/// Create a new character.
		/// </summary>
		/// <param name="request">The character creation request.</param>
		/// <returns>Created character.</returns>
		[Authorize]
		[HttpPost]
		public async Task<CharacterDto> CreateCharacterAsync(CreateCharacterRequest request)
		{
			var character = await mediator.Send(new CreateCharacterCommand(request.OwnerId, request.Name, request.AncestryId, request.BackgroundId, request.ClassId));
			return mapper.Map<CharacterDto>(character);
		}

		/// <summary>
		/// Delete an existing character.
		/// </summary>
		/// <param name="characterId">The character ID for deletion.</param>
		[Authorize]
		[HttpDelete("{characterId}")]
		public async Task DeleteCharacterAsync([FromRoute] Guid characterId)
		{
			await mediator.Send(new DeleteCharacterCommand(HttpContext.GetUserId(), characterId));
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
		/// Find character by ID.
		/// </summary>
		/// <param name="characterId">Character ID to retrieve.</param>
		/// <returns>Specified character.</returns>
		[Authorize]
		[HttpGet("{characterId}")]
		public async Task<CharacterDto> GetCharacterAsync([FromRoute] Guid characterId)
		{
			var character = await mediator.Send(new GetCharacterQuery(characterId));
			return mapper.Map<CharacterDto>(character);
		}

		/// <summary>
		/// Change ability of the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">Dictionary with updated scores.</param>
		[Authorize]
		[HttpPatch("{characterId}/abilities")]
		public async Task EditCharacterAbilitiesAsync([FromRoute] Guid characterId, [FromBody] EditAbilitiesRequest request)
		{
			await mediator.Send(new EditAbilitiesCommand(HttpContext.GetUserId(), characterId, request.Scores));
		}

		/// <summary>
		/// Change skills of the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">Dictionary with updated proficiencies.</param>
		[Authorize]
		[HttpPatch("{characterId}/skills")]
		public async Task EditCharacterSkillsAsync([FromRoute] Guid characterId, [FromBody] EditSkillsRequest request)
		{
			await mediator.Send(new EditSkillsCommand(HttpContext.GetUserId(), characterId, request.Proficiencies));
		}

		/// <summary>
		/// Change perception of the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The perception edit request.</param>
		/// <returns>Changed perception.</returns>
		[Authorize]
		[HttpPatch("{characterId}/perception")]
		public async Task<Perception> EditCharacterPerceptionAsync([FromRoute] Guid characterId, [FromBody] EditPerceptionRequest request)
		{
			return await mediator.Send(new EditPerceptionCommand(HttpContext.GetUserId(), characterId, request.Proficiency));
		}

		/// <summary>
		/// Change saving throw of the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">Dictionary with updated proficiencies.</param>
		[Authorize]
		[HttpPatch("{characterId}/saving-throws")]
		public async Task EditCharacterSavingThrowsAsync([FromRoute] Guid characterId, [FromBody] EditSavingThrowsRequest request)
		{
			await mediator.Send(new EditSavingThrowsCommand(HttpContext.GetUserId(), characterId, request.Proficiencies));
		}

		/// <summary>
		/// Change armor settings and proficiencies of the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The armor proficiency edit request.</param>
		[Authorize]
		[HttpPatch("{characterId}/armor")]
		public async Task EditCharacterArmorAsync([FromRoute] Guid characterId, [FromBody] EditArmorRequest request)
		{
			await mediator.Send(new EditArmorCommand(HttpContext.GetUserId(), characterId, request.Type, request.Bonus, request.HasDexterityCap, request.DexterityCap, request.Proficiencies));
		}

		/// <summary>
		/// Change health of the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The health edit request.</param>
		/// <returns>Changed health.</returns>
		[Authorize]
		[HttpPatch("{characterId}/health")]
		public async Task<Health> EditCharacterHealthAsync([FromRoute] Guid characterId, [FromBody] EditHealthRequest request)
		{
			return await mediator.Send(new EditHealthCommand(HttpContext.GetUserId(), characterId, request.Current, request.Max, request.Temporary));
		}

		/// <summary>
		/// Apply heal or damage to the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The health modify request.</param>
		/// <returns>Modified health.</returns>
		[Authorize]
		[HttpPatch("{characterId}/health-modify")]
		public async Task<Health> ModifyCharacterHealthAsync([FromRoute] Guid characterId, [FromBody] ModifyHealthRequest request)
		{
			return await mediator.Send(new ModifyHealthCommand(HttpContext.GetUserId(), characterId, request.Change));
		}
	}
}

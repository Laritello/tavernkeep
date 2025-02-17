﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter;
using Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter;
using Tavernkeep.Application.UseCases.Characters.Commands.EditAbilities;
using Tavernkeep.Application.UseCases.Characters.Commands.EditArmor;
using Tavernkeep.Application.UseCases.Characters.Commands.EditConditions;
using Tavernkeep.Application.UseCases.Characters.Commands.EditHealth;
using Tavernkeep.Application.UseCases.Characters.Commands.EditHeroPoints;
using Tavernkeep.Application.UseCases.Characters.Commands.EditInformation;
using Tavernkeep.Application.UseCases.Characters.Commands.EditPerception;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrows;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSkills;
using Tavernkeep.Application.UseCases.Characters.Commands.EditSpeeds;
using Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth;
using Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacterTemplate;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Character.Requests;
using Tavernkeep.Server.Extensions;

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
		public async Task<CharacterDto> CreateCharacterAsync(CharacterTemplateDto request)
		{
			var character = await mediator.Send(new CreateCharacterCommand(HttpContext.GetUserId(), request));
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
		/// Fetch character template.
		/// </summary>
		/// <returns>Basic character template.</returns>
		[Authorize]
		[HttpGet("template")]
		public async Task<CharacterTemplateDto> GetCharacterTemplateAsync()
		{
			var character = await mediator.Send(new GetCharacterTemplateQuery());
			return mapper.Map<CharacterTemplateDto>(character);
		}

		/// <summary>
		/// Change general information.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="information">Updated information abouth the character.</param>
		[Authorize]
		[HttpPatch("{characterId}/information")]
		public async Task EditInformationAsync([FromRoute] Guid characterId, [FromBody] CharacterInformationEditDto information)
		{
			await mediator.Send(new EditInformationCommand(HttpContext.GetUserId(), characterId, information));
		}

		/// <summary>
		/// Change hero points amount.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="amount">New hero points amount.</param>
		[Authorize]
		[HttpPatch("{characterId}/hero-points")]
		public async Task EditHeroPointsAsync([FromRoute] Guid characterId, [FromQuery] int amount)
		{
			await mediator.Send(new EditHeroPointsCommand(HttpContext.GetUserId(), characterId, amount));
		}

		/// <summary>
		/// Change ability scores.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">Dictionary with updated scores.</param>
		[Authorize]
		[HttpPatch("{characterId}/abilities")]
		public async Task EditAbilitiesAsync([FromRoute] Guid characterId, [FromBody] EditAbilitiesRequest request)
		{
			await mediator.Send(new EditAbilitiesCommand(HttpContext.GetUserId(), characterId, request.Scores));
		}

		/// <summary>
		/// Change skills proficiencies.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">Dictionary with updated proficiencies.</param>
		[Authorize]
		[HttpPatch("{characterId}/skills")]
		public async Task EditSkillsAsync([FromRoute] Guid characterId, [FromBody] EditSkillsRequest request)
		{
			await mediator.Send(new EditSkillsCommand(HttpContext.GetUserId(), characterId, request.Skills));
		}

		/// <summary>
		/// Change perception proficiency.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The perception edit request.</param>
		[Authorize]
		[HttpPatch("{characterId}/perception")]
		public async Task EditPerceptionAsync([FromRoute] Guid characterId, [FromBody] EditPerceptionRequest request)
		{
			await mediator.Send(new EditPerceptionCommand(HttpContext.GetUserId(), characterId, request.Proficiency));
		}

		/// <summary>
		/// Change saving throw proficiencies.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">Dictionary with updated proficiencies.</param>
		[Authorize]
		[HttpPatch("{characterId}/saving-throws")]
		public async Task EditSavingThrowsAsync([FromRoute] Guid characterId, [FromBody] EditSavingThrowsRequest request)
		{
			await mediator.Send(new EditSavingThrowsCommand(HttpContext.GetUserId(), characterId, request.Proficiencies));
		}

		/// <summary>
		/// Change armor settings and proficiencies.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The armor proficiency edit request.</param>
		[Authorize]
		[HttpPatch("{characterId}/armor")]
		public async Task EditArmorAsync([FromRoute] Guid characterId, [FromBody] EditArmorRequest request)
		{
			await mediator.Send(new EditArmorCommand(HttpContext.GetUserId(), characterId, request.Type, request.Bonus, request.HasDexterityCap, request.DexterityCap, request.Proficiencies));
		}

		/// <summary>
		/// Change speeds.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The armor proficiency edit request.</param>
		[Authorize]
		[HttpPatch("{characterId}/speeds")]
		public async Task EditSpeedAsync([FromRoute] Guid characterId, [FromBody] EditSpeedsRequest request)
		{
			await mediator.Send(new EditSpeedsCommand(HttpContext.GetUserId(), characterId, request.Speeds));
		}

		/// <summary>
		/// Change applied condition.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The request, containing applied conditions.</param>
		[Authorize]
		[HttpPatch("{characterId}/conditions")]
		public async Task EditConditionsAsync([FromRoute] Guid characterId, [FromBody] EditConditionsRequest request)
		{
			await mediator.Send(new EditConditionsCommand(HttpContext.GetUserId(), characterId, request.Conditions));
		}

		/// <summary>
		/// Change health parameters.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The health edit request.</param>
		[Authorize]
		[HttpPatch("{characterId}/health")]
		public async Task EditHealthAsync([FromRoute] Guid characterId, [FromBody] EditHealthRequest request)
		{
			await mediator.Send(new EditHealthCommand(HttpContext.GetUserId(), characterId, request.Current, request.Temporary));
		}

		/// <summary>
		/// Apply heal or damage.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The health modify request.</param>
		[Authorize]
		[HttpPatch("{characterId}/health-modify")]
		public async Task ModifyHealthAsync([FromRoute] Guid characterId, [FromBody] ModifyHealthRequest request)
		{
			await mediator.Send(new ModifyHealthCommand(HttpContext.GetUserId(), characterId, request.Change));
		}

		/// <summary>
		/// Perform long rest.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="restWithoutComfort">Reduces healing by half if true.</param>
		/// <param name="sleepInArmor">Applies fatigue condition if true.</param>
		[Authorize]
		[HttpPatch("{characterId}/long-rest")]
		public async Task PerofrmLongRestAsync([FromRoute] Guid characterId, [FromQuery] bool restWithoutComfort, [FromQuery] bool sleepInArmor)
		{
			await mediator.Send(new PerformLongRestCommand(HttpContext.GetUserId(), characterId, restWithoutComfort, sleepInArmor));
		}
	}
}

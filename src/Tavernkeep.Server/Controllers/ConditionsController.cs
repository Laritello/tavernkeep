using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Conditions.Commands.ApplyCondition;
using Tavernkeep.Application.UseCases.Conditions.Commands.EditConditions;
using Tavernkeep.Application.UseCases.Conditions.Commands.RemoveCondition;
using Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition;
using Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Conditions.Dtos;
using Tavernkeep.Core.Contracts.Conditions.Request;
using Tavernkeep.Server.Extensions;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	/// The <see cref="ConditionsController"/> class handles character operations within the application.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	/// <param name="mapper">The <see cref="IMapper"/> instance.</param>
	[ApiController]
	[Route("/api/[controller]")]
	public class ConditionsController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Get all conditions.
		/// </summary>
		/// <returns>List containing all conditions.</returns>
		[Authorize]
		[HttpGet]
		public async Task<List<ConditionTemplateDto>> GetAllConditionsAsync()
		{
			var conditions = await mediator.Send(new GetConditionsQuery());
			return mapper.Map<List<ConditionTemplateDto>>(conditions);
		}

		/// <summary>
		/// Get condition by name.
		/// </summary>
		/// <param name="name">The name of specified condition.</param>
		/// <returns>Specified condition.</returns>
		[Authorize]
		[HttpGet("{name}")]
		public async Task<ConditionTemplateDto> GetConditionAsync([FromRoute] string name)
		{
			var condition = await mediator.Send(new GetConditionQuery(name));
			return mapper.Map<ConditionTemplateDto>(condition);
		}

		/// <summary>
		/// Apply condition to the character.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Updated character.</returns>
		[Authorize]
		[HttpPost("apply")]
		public async Task<CharacterDto> ApplyConditionAsync([FromBody] ApplyConditionRequest request)
		{
			var character = await mediator.Send(new ApplyConditionCommand(HttpContext.GetUserId(), request.CharacterId, request.ConditionName, request.ConditionLevel));
			return mapper.Map<CharacterDto>(character);
		}

		/// <summary>
		/// Apply condition to the character.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The request.</param>
		[Authorize]
		[HttpPatch("{characterId}")]
		public async Task EditConditionsAsync([FromRoute] Guid characterId, [FromBody] EditConditionsRequest request)
		{
			await mediator.Send(new EditConditionsCommand(HttpContext.GetUserId(), characterId, request.Conditions));
		}

		/// <summary>
		/// Remove condition from the character.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Updated character.</returns>
		[Authorize]
		[HttpDelete("remove")]
		public async Task<CharacterDto> RemoveConditionAsync([FromBody] RemoveConditionRequest request)
		{
			var character = await mediator.Send(new RemoveConditionCommand(HttpContext.GetUserId(), request.CharacterId, request.ConditionName));
			return mapper.Map<CharacterDto>(character);
		}
	}
}

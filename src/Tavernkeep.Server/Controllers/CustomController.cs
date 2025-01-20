using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Custom.Commands.AddCustomSkill;
using Tavernkeep.Application.UseCases.Custom.Commands.DeleteCustomSkill;
using Tavernkeep.Core.Contracts.Character.Requests;
using Tavernkeep.Server.Extensions;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	/// The <see cref="CustomController"/> class handles operations with custom skills.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	[ApiController]
	[Route("/api/[controller]")]
	public class CustomController(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Add custom skill.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="request">The custom skill request.</param>
		/// <returns>Modified health.</returns>
		[Authorize]
		[HttpPost("skill")]
		public async Task AddCustomSkill([FromQuery] Guid characterId, [FromBody] AddCustomSkillRequest request)
		{
			await mediator.Send(new AddCustomSkillCommand(HttpContext.GetUserId(), characterId, request.Name, request.BaseAbility, request.Type));
		}

		/// <summary>
		/// Delete custom skill.
		/// </summary>
		/// <param name="characterId">Character ID to target.</param>
		/// <param name="skillName">The name of the skill.</param>
		/// <returns>Modified health.</returns>
		[Authorize]
		[HttpDelete("skill")]
		public async Task DeleteCustomSkill([FromQuery] Guid characterId, [FromQuery] string skillName)
		{
			await mediator.Send(new DeleteCustomSkillCommand(HttpContext.GetUserId(), characterId, skillName));
		}
	}
}

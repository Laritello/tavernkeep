using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Lores.Commands.CreateLore;
using Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore;
using Tavernkeep.Application.UseCases.Lores.Commands.EditLore;
using Tavernkeep.Core.Contracts.Lores.Requests;
using Tavernkeep.Core.Entities;
using Tavernkeep.Server.Extensions;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	/// The <see cref="LoreController"/> class handles character's lore skill operations within the application.
	/// </summary>
	/// <param name="mediator"></param>
	[ApiController]
	[Route("/api/[controller]")]
	public class LoreController(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Add a lore skill to the character.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public async Task<Lore> CreateLoreAsync([FromBody] CreateLoreRequest request)
		{
			return await mediator.Send(new CreateLoreCommand(HttpContext.GetUserId(), request.CharacterId, request.Topic, request.Proficiency));
		}

		/// <summary>
		/// Edit a lore skill of the character.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPatch]
		public async Task<Lore> EditLoreAsync([FromBody] EditLoreRequest request)
		{
			return await mediator.Send(new EditLoreCommand(HttpContext.GetUserId(), request.CharacterId, request.Topic, request.Proficiency));
		}

		/// <summary>
		/// Remove a lore skill from the character.
		/// </summary>
		/// <param name="characterId">Owner of the lore skill.</param>
		/// <param name="topic">Lore topic.</param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{characterId}")]
		public async Task DeleteLoreAsync([FromRoute] Guid characterId, [FromQuery] string topic)
		{
			await mediator.Send(new DeleteLoreCommand(HttpContext.GetUserId(), characterId, topic));
		}
	}
}

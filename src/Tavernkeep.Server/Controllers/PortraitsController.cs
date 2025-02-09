using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Portraits.Commands.UpdatePortrait;
using Tavernkeep.Application.UseCases.Portraits.Queries.GetPortrait;
using Tavernkeep.Server.Extensions;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	/// The <see cref="PortraitsController"/> class handles portrait operations within the application.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class PortraitsController(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Fetch portrait for the specified character.
		/// </summary>
		/// <param name="characterId">The ID of portrait owner.</param>
		/// <returns>Image containing the portrait of the character.</returns>
		[AllowAnonymous]
		[HttpGet("{characterId}")]
		public async Task<IActionResult> GetPortraitAsync([FromRoute] Guid characterId)
		{
			var portrait = await mediator.Send(new GetPortraitQuery(characterId));
			return File(portrait.Bytes, portrait.MimeType);
		}

		/// <summary>
		/// Add or update portrait for the specified character.
		/// </summary>
		/// <param name="characterId">The character creation request.</param>
		/// <param name="file">File data.</param>
		/// <returns>Created character.</returns>
		[Authorize]
		[HttpPost("{characterId}")]
		public async Task UpdatePortraitAsync([FromRoute] Guid characterId, IFormFile file)
		{
			await mediator.Send(new UpdatePortraitCommand(HttpContext.GetUserId(), characterId, file));
		}
	}
}

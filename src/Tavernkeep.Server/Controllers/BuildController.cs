using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Builds.Queries.GetCharacterBuild;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	///  The <see cref="BuildController"/> class that handles characters' build.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class BuildController(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Get build for the character.
		/// </summary>
		/// <param name="characterId">Character's ID.</param>
		/// <returns>Character's build.</returns>
		[HttpGet("{characterId}")]
		public async Task<Build> GetCharacterBuildAsync([FromRoute] Guid characterId)
		{
			var build = await mediator.Send(new GetCharacterBuildQuery(characterId));
			return build;
		}
	}
}

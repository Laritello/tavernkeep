using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Builds.Commands.UpdateCharacterBuild;
using Tavernkeep.Application.UseCases.Builds.Queries.GetCharacterBuild;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	///  The <see cref="BuildController"/> class that handles characters' build.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	/// <param name="mapper">The <see cref="IMapper"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class BuildController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Get build for the character.
		/// </summary>
		/// <param name="characterId">Character's ID.</param>
		/// <returns>Character's build.</returns>
		[HttpGet("{characterId}")]
		public async Task<BuildDetails> GetCharacterBuildAsync([FromRoute] Guid characterId)
		{
			var build = await mediator.Send(new GetCharacterBuildQuery(characterId));
			return build;
		}

		/// <summary>
		/// Update character's build.
		/// </summary>
		/// <param name="characterId">Character's ID.</param>
		/// <param name="build">Build.</param>
		/// <returns>Updated character.</returns>
		[HttpPatch("{characterId}")]
		public async Task<CharacterDto> UpdateBuildAsync([FromRoute] Guid characterId, [FromBody] BuildDetails build)
		{
			var character = await mediator.Send(new UpdateCharacterBuildCommand(characterId, build));
			return mapper.Map<CharacterDto>(character);
		}
	}
}

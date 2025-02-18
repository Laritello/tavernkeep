using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Creatures.Queries.GetCreature;
using Tavernkeep.Application.UseCases.Creatures.Queries.GetCreatures;
using Tavernkeep.Core.Contracts.Creatures;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	/// The <see cref="LibraryController"/> class handles library operations.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	/// <param name="mapper">The <see cref="IMapper"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class LibraryController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Fetch all creatures.
		/// </summary>
		/// <returns>List containing all conditions.</returns>
		[Authorize]
		[HttpGet("creatures")]
		public async Task<ICollection<CreatureDto>> GetAllCreatures()
		{
			var conditions = await mediator.Send(new GetCreaturesQuery());
			return mapper.Map<ICollection<CreatureDto>>(conditions);
		}

		/// <summary>
		/// Fetch creature by ID.
		/// </summary>
		/// <param name="creatureId">ID of the creature to fetch.</param>
		/// <returns>List containing all conditions.</returns>
		[Authorize]
		[HttpGet("creatures/{creatureId}")]
		public async Task<CreatureFullDto> GetAllCreatures([FromRoute] Guid creatureId)
		{
			var conditions = await mediator.Send(new GetCreatureQuery(creatureId));
			return mapper.Map<CreatureFullDto>(conditions);
		}
	}
}

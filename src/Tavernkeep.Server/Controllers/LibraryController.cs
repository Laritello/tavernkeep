using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions;
using Tavernkeep.Application.UseCases.Creatures.Queries;
using Tavernkeep.Core.Contracts.Conditions.Dtos;
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
		/// Get all conditions.
		/// </summary>
		/// <returns>List containing all conditions.</returns>
		[Authorize]
		[HttpGet("creatures")]
		public async Task<ICollection<CreatureDto>> GetAllCreatures()
		{
			var conditions = await mediator.Send(new GetCreaturesQuery());
			return mapper.Map<ICollection<CreatureDto>>(conditions);
		}
	}
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter;
using Tavernkeep.Core.Contracts.Encounters.Dtos;
using Tavernkeep.Core.Contracts.Enums;
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
	public class EncountersController(IMediator mediator, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Get all characters.
		/// </summary>
		/// <returns>List containing all characters.</returns>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpPost]
		public async Task<EncounterDto> CreateEncounterAsync(string name)
		{
			var encounter = await mediator.Send(new CreateEncounterCommand(HttpContext.GetUserId(), name));
			return mapper.Map<EncounterDto>(encounter);
		}
	}
}

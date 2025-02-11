using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters;
using Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter;
using Tavernkeep.Application.UseCases.Encounters.Queries.GetAllEncounters;
using Tavernkeep.Application.UseCases.Encounters.Queries.GetEncounter;
using Tavernkeep.Core.Contracts.Character.Dtos;
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
		/// Get all encounters.
		/// </summary>
		/// <returns>List containing all encounters.</returns>
		[Authorize]
		[HttpGet]
		public async Task<Dictionary<Guid, EncounterDto>> GetEncountersAsync()
		{
			var characters = await mediator.Send(new GetAllEncounterQuery());
			return mapper.Map<Dictionary<Guid, EncounterDto>>(characters);
		}

		/// <summary>
		/// Fetch encounter by ID.
		/// </summary>
		/// <param name="encounterId">Encounter ID to fetch.</param>
		/// <returns>Specified encounter.</returns>
		[Authorize]
		[HttpGet("{encounterId}")]
		public async Task<EncounterDto> GetEncounterAsync([FromRoute] Guid encounterId)
		{
			var character = await mediator.Send(new GetEncounterQuery(encounterId));
			return mapper.Map<EncounterDto>(character);
		}

		/// <summary>
		/// Create an encounter.
		/// </summary>
		/// <returns>List containing all characters.</returns>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpPost]
		public async Task<EncounterDto> CreateEncounterAsync([FromQuery] string name)
		{
			var encounter = await mediator.Send(new CreateEncounterCommand(name));
			return mapper.Map<EncounterDto>(encounter);
		}
	}
}

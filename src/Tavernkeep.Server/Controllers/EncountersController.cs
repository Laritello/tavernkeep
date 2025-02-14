using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters;
using Tavernkeep.Application.UseCases.Encounters.Commands.AddEncounterParticipant;
using Tavernkeep.Application.UseCases.Encounters.Commands.ClearInitiative;
using Tavernkeep.Application.UseCases.Encounters.Commands.CreateEncounter;
using Tavernkeep.Application.UseCases.Encounters.Commands.EditEncounterStatus;
using Tavernkeep.Application.UseCases.Encounters.Commands.RemoveEncounterParticipant;
using Tavernkeep.Application.UseCases.Encounters.Commands.UpdateParticipantsOrdinal;
using Tavernkeep.Application.UseCases.Encounters.Queries.GetAllEncounters;
using Tavernkeep.Application.UseCases.Encounters.Queries.GetEncounter;
using Tavernkeep.Application.UseCases.Rolls.Commands.RollEncounterInitiative;
using Tavernkeep.Application.UseCases.Rolls.Commands.RollEncounterParticipantInitiative;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Encounters.Dtos;
using Tavernkeep.Core.Contracts.Encounters.Requests;
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

		/// <summary>
		/// Add participant to the encounter
		/// </summary>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpPost("{encounterId}/participant")]
		public async Task AddToEncounterAsync([FromRoute] Guid encounterId, [FromBody] AddEncounterParticipantRequest request)
		{
			await mediator.Send(new AddEncounterParticipantCommand(encounterId, request.Type, request.EntityId));
		}

		/// <summary>
		/// Remove participant from the encounter
		/// </summary>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpDelete("{encounterId}/participant/{participantId}")]
		public async Task RemoveFromEncounterAsync([FromRoute] Guid encounterId, [FromRoute] Guid participantId)
		{
			await mediator.Send(new RemoveEncounterParticipantCommand(encounterId, participantId));
		}

		/// <summary>
		/// Change participants ordinal
		/// </summary>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpPatch("{encounterId}/ordinal")]
		public async Task UpdateParticipantsOrdinalAsync([FromRoute] Guid encounterId, [FromBody] IList<Guid> ordinals)
		{
			await mediator.Send(new UpdateParticipantsOrdinalCommand(encounterId, ordinals));
		}

		/// <summary>
		/// Update encounter status
		/// </summary>
		/// <param name="encounterId">Encounter ID to update status for.</param>
		/// <param name="status">New status of the encounter.</param>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpPatch("{encounterId}/status")]
		public async Task UpdateEncounterStatusAsync([FromRoute] Guid encounterId, [FromQuery] EncounterStatus status)
		{
			await mediator.Send(new EditEncounterStatusCommand(encounterId, status));
		}

		/// <summary>
		/// Roll initiative for encounter.
		/// </summary>
		/// <param name="encounterId">Encounter ID to roll initiative for.</param>
		/// <param name="npcOnly">Roll initiative only for NPCs.</param>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpPatch("{encounterId}/initiative")]
		public async Task RollInitiativeAsync([FromRoute] Guid encounterId, [FromQuery] bool npcOnly)
		{
			await mediator.Send(new RollEncounterInitiativeCommand(HttpContext.GetUserId(), encounterId, npcOnly));
		}

		/// <summary>
		/// Clear initiative for the encounter.
		/// </summary>
		/// <param name="encounterId">Encounter ID to roll initiative for.</param>
		[Authorize]
		[RequiresRole(UserRole.Master)]
		[HttpDelete("{encounterId}/initiative")]
		public async Task ClearInitiativeAsync([FromRoute] Guid encounterId)
		{
			await mediator.Send(new ClearInitiativeCommand(encounterId));
		}

		/// <summary>
		/// Roll initiative for specific participant
		/// </summary>
		/// <param name="encounterId">Encounter ID to roll initiative for.</param>
		/// <param name="participantId">Participant ID</param>
		/// <param name="initiativeSkill">Name of the skill used for initiative roll.</param>
		[Authorize]
		[HttpPatch("{encounterId}/initiative/{participantId}")]
		public async Task RollInitiativeAsync([FromRoute] Guid encounterId, [FromRoute] Guid participantId, [FromQuery] string initiativeSkill)
		{
			await mediator.Send(new RollEncounterParticipantInitiativeCommand(HttpContext.GetUserId(), encounterId, participantId, initiativeSkill));
		}
	}
}

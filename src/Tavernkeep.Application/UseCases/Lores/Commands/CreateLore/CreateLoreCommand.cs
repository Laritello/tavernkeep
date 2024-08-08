﻿using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Lores.Commands.CreateLore
{
	public class CreateLoreCommand(Guid initiatorId, Guid characterId, string topic, Proficiency proficiency) : IRequest<Lore>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string Topic { get; set; } = topic;
		public Proficiency Proficiency { get; set; } = proficiency;
	}
}

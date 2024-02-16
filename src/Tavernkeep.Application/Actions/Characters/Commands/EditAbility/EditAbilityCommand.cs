﻿using MediatR;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.Actions.Characters.Commands.EditAbility
{
    public class EditAbilityCommand : IRequest<Ability>
    {
        public Guid InitiatorId { get; set; }
        public Guid CharacterId { get; set; }
        public AbilityType Type { get; set; }
        public int Score { get; set; }

        public EditAbilityCommand(Guid initiatorId, Guid characterId, AbilityType type, int score)
        {
            InitiatorId = initiatorId;
            CharacterId = characterId;
            Type = type;
            Score = score;
        }
    }
}

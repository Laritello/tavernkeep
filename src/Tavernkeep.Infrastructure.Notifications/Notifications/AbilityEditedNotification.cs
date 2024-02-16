﻿using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Infrastructure.Notifications.Notifications
{
    public class AbilityEditedNotification
    {
        public Guid CharacterId { get; set; }
        public AbilityType Type { get; set; }
        public int Score { get; set; }

        public AbilityEditedNotification()
        {

        }

        public AbilityEditedNotification(Guid characterId, AbilityType type, int score)
        {
            CharacterId = characterId;
            Type = type;
            Score = score;
        }
    }
}

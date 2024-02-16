using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Infrastructure.Notifications.Notifications
{
    public class SkillEditedNotification
    {
        public Guid CharacterId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SkillType Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Proficiency Proficiency { get; set; }

        public SkillEditedNotification()
        {

        }

        public SkillEditedNotification(Guid characterId, SkillType type, Proficiency proficiency)
        {
            CharacterId = characterId;
            Type = type;
            Proficiency = proficiency;
        }
    }
}

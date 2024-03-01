using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Infrastructure.Notifications.Notifications
{
    /// <summary>
    /// Notification that represents the event of <see cref="Skill"/> update.
    /// </summary>
    public class SkillEditedNotification
    {
        /// <summary>
        /// Id of the parent <see cref="Character"/> instance.
        /// </summary>
        public Guid CharacterId { get; set; }

        /// <summary>
        /// Type of the changed skill.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SkillType Type { get; set; }

        /// <summary>
        /// Updated proficiency for the ability.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Proficiency Proficiency { get; set; }
    }
}

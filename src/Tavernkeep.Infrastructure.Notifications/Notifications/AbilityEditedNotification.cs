using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Infrastructure.Notifications.Notifications
{
    /// <summary>
    /// Notification that represents the event of <see cref="Ability"/> update.
    /// </summary>
    public class AbilityEditedNotification
    {
        /// <summary>
        /// Id of the parent <see cref="Character"/> instance.
        /// </summary>
        public Guid CharacterId { get; set; }

        /// <summary>
        /// Type of the changed ability.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AbilityType Type { get; set; }

        /// <summary>
        /// Updated score for the ability.
        /// </summary>
        public int Score { get; set; }
    }
}

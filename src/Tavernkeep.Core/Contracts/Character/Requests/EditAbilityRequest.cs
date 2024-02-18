using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
    /// <summary>
    /// Represents a request to edit the ability of a character.
    /// </summary>
    public class EditAbilityRequest
    {
        /// <summary>
        /// The ID of the character.
        /// </summary>
        public Guid CharacterId { get; set; } = default!;

        /// <summary>
        /// The type of the ability.
        /// </summary>
        public AbilityType Type { get; set; } = default!;

        /// <summary>
        /// The score of the ability.
        /// </summary>
        public int Score { get; set; } = default!;
    }
}

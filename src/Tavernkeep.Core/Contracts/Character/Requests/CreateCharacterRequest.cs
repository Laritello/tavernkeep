namespace Tavernkeep.Core.Contracts.Character.Requests
{
    /// <summary>
    /// Represents a request to create a character.
    /// </summary>
    public class CreateCharacterRequest
    {
        /// <summary>
        /// The name of the character.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}

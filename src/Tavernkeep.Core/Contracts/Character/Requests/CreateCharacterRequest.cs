namespace Tavernkeep.Core.Contracts.Character.Requests
{
    public class CreateCharacterRequest
    {
        public Guid OwnerId { get; set; }
        public string CharacterName { get; set; } = default!;
    }
}

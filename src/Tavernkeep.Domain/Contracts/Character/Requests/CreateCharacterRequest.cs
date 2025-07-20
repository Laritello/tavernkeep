using Tavernkeep.Domain.Contracts.Character.Dtos;

namespace Tavernkeep.Domain.Contracts.Character.Requests;

/// <summary>
/// Represents a request to create a character.
/// </summary>
[Obsolete]
public class CreateCharacterRequest
{
	/// <summary>
	/// The ID of the owner.
	/// </summary>
	public Guid OwnerId { get; set; }

	/// <summary>
	/// The character DTO.
	/// </summary>
	public CharacterTemplateDto Character { get; set; } = default!;
}

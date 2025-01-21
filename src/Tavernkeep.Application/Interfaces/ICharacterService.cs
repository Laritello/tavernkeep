using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.Interfaces
{
	public interface ICharacterService
	{
		/// <summary>
		/// Creates a new character with the specified details and associates it with the given owner.
		/// </summary>
		/// <param name="owner">The user who owns the character.</param>
		/// <param name="name">The name of the character.</param>
		/// <param name="ancestryId">The ID of the character's ancestry.</param>
		/// <param name="backgroundId">The ID of the character's background.</param>
		/// <param name="classId">The ID of the character's class.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A task representing the newly created character.</returns>
		public Task<Character> CreateCharacterAsync(User owner, string name, string ancestryId, string backgroundId, string classId, CancellationToken cancellationToken);

		/// <summary>
		/// Saves or updates an existing character in the database.
		/// </summary>
		/// <param name="character">The character to save or update.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A task representing the completion of the save operation.</returns>
		public Task SaveCharacter(Character character, CancellationToken cancellationToken);

		/// <summary>
		/// Deletes a character from the database.
		/// </summary>
		/// <param name="character">The character to delete.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A task representing the completion of the deletion.</returns>
		public Task DeleteCharacter(Character character, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieves a character by its unique ID.
		/// </summary>
		/// <param name="id">The GUID of the character to retrieve.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A task representing the retrieved character.</returns>
		public Task<Character> GetCharacterAsync(Guid id, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieves all characters in the database.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A task representing a list of all characters.</returns>
		public Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Validates permissions and retrieves a character for editing.
		/// </summary>
		/// <param name="characterId">The GUID of the character to retrieve.</param>
		/// <param name="userId">The GUID of the user requesting the edit.</param>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>A task representing the character retrieved for editing.</returns>
		public Task<Character> RetrieveCharacterForEdit(Guid characterId, Guid userId, CancellationToken cancellationToken);
	}
}

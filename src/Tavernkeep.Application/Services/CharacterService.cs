using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Services
{
	// TODO: Optimize and refactor!!!
	public class CharacterService(
		ICharacterRepository characterRepository,
		IUserRepository userRepository,
		INotificationService notificationService) : ICharacterService
	{
		public async Task<Character> CreateCharacterAsync(User owner, string name, string ancestryId, string backgroundId, string classId, CancellationToken cancellationToken = default)
		{
			Character character = new()
			{
				Owner = owner,
				Name = name,
			};

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);

			return character;
		}

		public async Task<List<Character>> GetAllCharactersAsync(CancellationToken cancellationToken)
		{
			return await characterRepository.GetAllCharactersAsync(cancellationToken);
		}

		public async Task<Character> GetCharacterAsync(Guid id, CancellationToken cancellationToken)
		{
			var character = await characterRepository.FindAsync(id, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("No character with provided ID found.");

			return character;
		}

		public async Task<Character> RetrieveCharacterForEdit(Guid characterId, Guid userId, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(userId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(characterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != userId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			return character;
		}

		public async Task SaveCharacter(Character character, CancellationToken cancellationToken)
		{
			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}

		public async Task DeleteCharacter(Character character, CancellationToken cancellationToken)
		{
			characterRepository.Remove(character);
			await characterRepository.CommitAsync(cancellationToken);
			// TODO: Notification about character deletion
		}
	}
}

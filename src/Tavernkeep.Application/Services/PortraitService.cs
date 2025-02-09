using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Specifications.Characters;

namespace Tavernkeep.Application.Services
{
	public class PortraitService(
		ICharacterRepository characterRepository,
		IPortraitRepository portraitRepository
		) : IPortaitService
	{
		public async Task<Portrait?> GetPortraitAsync(Guid characterId, CancellationToken cancellationToken)
		{
			return await portraitRepository.FindAsync(characterId, cancellationToken: cancellationToken);
		}

		public async Task UpdatePortraitAsync(Guid characterId, byte[] bytes, string mimeType, CancellationToken cancellationToken)
		{
			var character = await characterRepository.FindAsync(characterId, new CharacterPortraitSpecification(characterId), cancellationToken)
				?? throw new BusinessLogicException("Character not found");

			if (character.Portrait != null)
			{
				character.Portrait.Bytes = bytes;
			}
			else
			{
				character.Portrait = new Portrait
				{
					Id = characterId,
					MimeType = mimeType,
					Bytes = bytes,
				};
			}

			characterRepository.Save(character);
			await portraitRepository.CommitAsync(cancellationToken);
		}
	}
}

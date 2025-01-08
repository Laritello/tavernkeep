using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Queries
{
	public class GetCharacterQueryTests
	{
		private readonly Guid characterId = Guid.NewGuid();
		private readonly Character character;

		public GetCharacterQueryTests()
		{
			character = new Character()
			{
				Id = characterId,
				Name = "Demo",
				Owner = new(string.Empty, string.Empty, UserRole.Player)
			};
		}

		[Test]
		public async Task GetCharacterQuery_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterService>();

			mockCharacterRepository
				.Setup(ser => ser.GetCharacterAsync(characterId, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new GetCharacterQuery(characterId);
			var handler = new GetCharacterQueryHandler(mockCharacterRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.That(response.Id, Is.EqualTo(characterId));
		}

		[Test]
		public void GetCharacterQuery_CharacterNotFound()
		{
			/*
			 * TODO: This tests doesn't make any sense and obsolete.
			 * When tests for services are 
			*/
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService.Setup(ser => ser.GetCharacterAsync(characterId, CancellationToken.None)).ThrowsAsync(new BusinessLogicException("No character with provided ID found."));

			var request = new GetCharacterQuery(characterId);
			var handler = new GetCharacterQueryHandler(mockCharacterService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("No character with provided ID found."));
		}
	}
}

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
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterService>();

			var request = new GetCharacterQuery(characterId);
			var handler = new GetCharacterQueryHandler(mockCharacterRepository.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("No character with provided ID found."));
		}
	}
}

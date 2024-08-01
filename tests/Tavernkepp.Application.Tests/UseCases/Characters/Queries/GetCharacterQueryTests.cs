using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tavernkeep.Application.UseCases.Characters.Commands.CreateCharacter;
using Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
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
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockCharacterRepository
                .Setup(repo => repo.FindAsync(characterId, default!))
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
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            var request = new GetCharacterQuery(characterId);
            var handler = new GetCharacterQueryHandler(mockCharacterRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("No character with provided ID found."));
        }
    }
}

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Application.Actions.Characters.Queries.GetCharacter;
using Tavernkeep.Application.Actions.Characters.Queries.GetCharacters;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Queries
{
    public class GetAllCharactersQueryTests
    {
        private readonly List<Character> characters;

        public GetAllCharactersQueryTests()
        {
            characters = [
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Demo 1",
                    Owner = new(string.Empty, string.Empty, UserRole.Player)
                },
                new Character()
                {
                    Id = Guid.NewGuid(),
                    Name = "Demo 2",
                    Owner = new(string.Empty, string.Empty, UserRole.Player)
                }];
        }

        [Test]
        public async Task GetAllCharactersCommand_Success()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCharacterRepository = new Mock<ICharacterRepository>();

            mockCharacterRepository
                .Setup(repo => repo.GetAllCharactersAsync(default!))
                .ReturnsAsync(characters);

            var request = new GetAllCharactersQuery();
            var handler = new GetAllCharactersQueryHandler(mockCharacterRepository.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.That(response, Has.Count.EqualTo(characters.Count));
        }
    }
}

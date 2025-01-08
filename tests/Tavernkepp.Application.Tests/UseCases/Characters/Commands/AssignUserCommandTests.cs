﻿using Moq;
using Tavernkeep.Application.UseCases.Characters.Commands.AssignUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Characters.Commands
{
	public class AssignUserCommandTests
	{
		private readonly Guid userId = Guid.NewGuid();
		private readonly Guid characterId = Guid.NewGuid();

		private readonly User owner;
		private Character character;

		public AssignUserCommandTests()
		{
			owner = new(string.Empty, string.Empty, UserRole.Player)
			{
				Id = userId,
			};
		}

		[SetUp]
		public void SetUp()
		{
			character = new();
		}

		[Test]
		public async Task AssignUserCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(userId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AssignUserCommand(characterId, userId);
			var handler = new AssignUserCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.That(response.Owner.Id, Is.EqualTo(userId));
		}

		[Test]
		public void AssignUserCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(userId, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new AssignUserCommand(characterId, userId);
			var handler = new AssignUserCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void AssignUserCommand_UserNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockCharacterRepository
				.Setup(repo => repo.FindAsync(characterId, It.IsAny<ISpecification<Character>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new AssignUserCommand(characterId, userId);
			var handler = new AssignUserCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}
	}
}

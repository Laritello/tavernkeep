﻿using Moq;
using Tavernkeep.Application.UseCases.Users.Commands.SetActiveCharacter;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
	public class SetActiveCharacterCommandTests
	{
		private readonly User master;
		private User owner;
		private Character character;

		public SetActiveCharacterCommandTests()
		{
			owner = new User(string.Empty, string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid(),
			};

			master = new User(string.Empty, string.Empty, UserRole.Master)
			{
				Id = Guid.NewGuid(),
			};

			character = new Character()
			{
				Owner = owner,
				Id = Guid.NewGuid()
			};
		}

		[SetUp]
		public void SetUp()
		{
			owner = new User(string.Empty, string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid(),
			};

			character = new Character()
			{
				Owner = owner,
				Id = Guid.NewGuid()
			};
		}

		[Test]
		public async Task SetActiveCharacterCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new SetActiveCharacterCommand(owner.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Id, Is.EqualTo(owner.Id));
				Assert.That(response.ActiveCharacter, Is.Not.Null);
				Assert.That(response.ActiveCharacter!.Id, Is.EqualTo(character.Id));
			});
		}

		[Test]
		public async Task SetActiveCharacterCommand_Success_Master()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new SetActiveCharacterCommand(master.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Id, Is.EqualTo(owner.Id));
				Assert.That(response.ActiveCharacter, Is.Not.Null);
				Assert.That(response.ActiveCharacter!.Id, Is.EqualTo(character.Id));
			});
		}

		[Test]
		public void SetActiveCharacterCommand_InitiatorNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new SetActiveCharacterCommand(owner.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Initiator with specified ID doesn't exist."));
		}

		[Test]
		public void SetActiveCharacterCommand_UserNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new SetActiveCharacterCommand(master.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}

		[Test]
		public void SetActiveCharacterCommand_CharacterNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			var request = new SetActiveCharacterCommand(owner.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}

		[Test]
		public void SetActiveCharacterCommand_UserIsNotOwner()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			character.Owner = new User(string.Empty, string.Empty, UserRole.Player);

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(master);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new SetActiveCharacterCommand(master.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character cannot be set as active character for the user that is not his owner."));
		}

		[Test]
		public void SetActiveCharacterCommand_NotEnoughPermissions()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(owner.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(owner);

			mockUserRepository
				.Setup(repo => repo.FindAsync(master.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new User(string.Empty, string.Empty, UserRole.Player));

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new SetActiveCharacterCommand(master.Id, owner.Id, character.Id);
			var handler = new SetActiveCharacterCommandHandler(mockUserRepository.Object, mockCharacterRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<InsufficientPermissionException>()
				.With.Message.EqualTo("You do not have the necessary permissions to perform this operation."));
		}
	}
}

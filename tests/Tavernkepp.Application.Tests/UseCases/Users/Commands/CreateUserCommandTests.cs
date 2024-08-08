using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Users.Commands.CreateUser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
	public class CreateUserCommandTests
	{
		private readonly string login = "login";
		private readonly string password = "password";
		private readonly UserRole role = UserRole.Player;
		private readonly string characterName = "character";

		private User user;
		private Character character;

		public CreateUserCommandTests()
		{
			user = new(login, password, role);
			character = new()
			{
				Name = characterName,
				Owner = user
			};
		}

		[Test]
		public async Task CreateUserCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			var request = new CreateUserCommand(login, password, role, false);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object, mockCharacterService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Login, Is.EqualTo(login));
				Assert.That(response.Password, Is.EqualTo(password));
				Assert.That(response.Role, Is.EqualTo(role));
			});
		}

		[Test]
		public async Task CreateUserCommand_Success_WithCharacter()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService.SetReturnsDefault(Task.FromResult(character));

			var request = new CreateUserCommand(login, password, UserRole.Player, true, characterName);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object, mockCharacterService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response.Login, Is.EqualTo(login));
				Assert.That(response.Password, Is.EqualTo(password));
				Assert.That(response.Role, Is.EqualTo(UserRole.Player));
				Assert.That(response.ActiveCharacter!.Name, Is.EqualTo(characterName));
			});
		}

		[Test]
		public void CreateUserCommand_EmptyLogin()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService.SetReturnsDefault(Task.FromResult(character));

			var request = new CreateUserCommand(string.Empty, password, UserRole.Player, true, characterName);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object, mockCharacterService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User can't have an empty login."));
		}

		[Test]
		public void CreateUserCommand_EmptyPassword()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterService = new Mock<ICharacterService>();

			mockCharacterService.SetReturnsDefault(Task.FromResult(character));

			var request = new CreateUserCommand(login, string.Empty, UserRole.Player, true, characterName);
			var handler = new CreateUserCommandHandler(mockUserRepository.Object, mockCharacterService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("User can't have an empty password."));
		}
	}
}

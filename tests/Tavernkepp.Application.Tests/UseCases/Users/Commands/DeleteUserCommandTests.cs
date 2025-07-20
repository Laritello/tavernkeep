using Moq;
using Tavernkeep.Application.UseCases.Users.Commands.DeleteUser;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities;
using Tavernkeep.Domain.Exceptions;
using Tavernkeep.Domain.Repositories;
using Tavernkeep.Domain.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Users.Commands
{
	public class DeleteUserCommandTests
	{
		private readonly User user;

		public DeleteUserCommandTests()
		{
			user = new User("user", "user", UserRole.Player) { Id = Guid.NewGuid() };
		}

		[Test]
		public async Task DeleteCharacterCommand_Success()
		{
			var mockUserRepository = new Mock<IUserRepository>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(user.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(user);

			var request = new DeleteUserCommand(user.Id);
			var handler = new DeleteUserCommandHandler(mockUserRepository.Object);

			await handler.Handle(request, CancellationToken.None);
		}

		[Test]
		public void DeleteCharacterCommand_UserNotFound()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();

			var request = new DeleteUserCommand(user.Id);
			var handler = new DeleteUserCommandHandler(mockUserRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}
	}
}

using Moq;
using Tavernkeep.Application.UseCases.Chat.Commands.DeleteMessage;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Chat.Commands
{
	public class DeleteMessageCommandTests
	{
		private readonly Message message;
		private readonly User sender;

		public DeleteMessageCommandTests()
		{
			sender = new(string.Empty, string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid()
			};

			message = new TextMessage()
			{
				Id = Guid.NewGuid(),
				Created = DateTime.UtcNow,
				SenderId = sender.Id,
				Sender = sender,
				Text = string.Empty
			};
		}

		[Test]
		public async Task DeleteMessageCommand_Success()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();
			mockMessageRepository
				.Setup(repo => repo.FindAsync(message.Id, It.IsAny<ISpecification<Message>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(message);

			var request = new DeleteMessageCommand(message.Id);
			var handler = new DeleteMessageCommandHandler(mockMessageRepository.Object, mockNotificationService.Object);

			await handler.Handle(request, CancellationToken.None);
		}

		[Test]
		public void DeleteMessageCommand_MessageNotFound()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			var request = new DeleteMessageCommand(message.Id);
			var handler = new DeleteMessageCommandHandler(mockMessageRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Message not found."));
		}
	}
}

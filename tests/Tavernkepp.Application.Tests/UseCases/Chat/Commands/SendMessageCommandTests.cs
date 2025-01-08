using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Chat.Commands.SendMessage;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Chat.Commands
{
	public class SendMessageCommandTests
	{
		private readonly User sender;
		private readonly User recipient;
		private readonly string text = "Lorem ipsum";

		public SendMessageCommandTests()
		{
			sender = new("user_sender", string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid()
			};

			recipient = new("user_recipient", string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid()
			};
		}

		[Test]
		public async Task SendMessageCommand_Success()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.GetDetailsAsync(sender.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(sender);

			var request = new SendMessageCommand(sender.Id, text);
			var handler = new SendMessageCommandHandler(mockMessageRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response, Is.AssignableFrom<TextMessage>());
				Assert.That(response.SenderId, Is.EqualTo(sender.Id));
				Assert.That(response.Sender.Id, Is.EqualTo(sender.Id));
				Assert.That(((TextMessage)response).Text, Is.EqualTo(text));
			});
		}

		[Test]
		public async Task SendMessageCommand_Success_WithRecipient()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.GetDetailsAsync(sender.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(sender);

			mockUserRepository
				.Setup(repo => repo.FindAsync(recipient.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(recipient);

			var request = new SendMessageCommand(sender.Id, text, recipient.Id);
			var handler = new SendMessageCommandHandler(mockMessageRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response, Is.AssignableFrom<TextMessage>());
				Assert.That(response.SenderId, Is.EqualTo(sender.Id));
				Assert.That(response.Sender.Id, Is.EqualTo(sender.Id));
				Assert.That(((TextMessage)response).RecipientId, Is.EqualTo(recipient.Id));
				Assert.That(((TextMessage)response).Recipient?.Id, Is.EqualTo(recipient.Id));
				Assert.That(((TextMessage)response).Text, Is.EqualTo(text));
			});
		}

		[Test]
		public void SendMessageCommand_EmptyText()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(sender.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(sender);

			var request = new SendMessageCommand(sender.Id, string.Empty);
			var handler = new SendMessageCommandHandler(mockMessageRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Text of the message cannot be empty."));
		}

		[Test]
		public void SendMessageCommand_SenderNotFound()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			var request = new SendMessageCommand(sender.Id, text);
			var handler = new SendMessageCommandHandler(mockMessageRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Sender with specified ID not found."));
		}

		[Test]
		public void SendMessageCommand_RecipientNotFound()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.GetDetailsAsync(sender.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(sender);

			var request = new SendMessageCommand(sender.Id, text, recipient.Id);
			var handler = new SendMessageCommandHandler(mockMessageRepository.Object, mockUserRepository.Object, mockNotificationService.Object);

			var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
			Assert.That(ex.Message, Is.EqualTo("Recipient with specified id not found."));
		}
	}
}

﻿using Moq;
using Tavernkeep.Application.UseCases.Chat.Queries.GetMessages;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Core.Specifications.Chat;

namespace Tavernkepp.Application.Tests.UseCases.Chat.Queries
{
	public class GetMessagesQueryTests
	{
		private readonly User initiator;
		private readonly List<Message> messages;

		public GetMessagesQueryTests()
		{
			initiator = new("user_initiator", string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid()
			};

			messages = [
				new TextMessage()
				{
					Id = Guid.NewGuid(),
					Created = DateTime.UtcNow,
					SenderId = Guid.NewGuid(),
					Text = "Message One"
				},
				new TextMessage()
				{
					Id = Guid.NewGuid(),
					Created = DateTime.UtcNow,
					SenderId = Guid.NewGuid(),
					Text = "Message Two"
				}];
		}

		[Test]
		public async Task GetMessagesQuery_Success()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			var chatSpecification = new ChatSpecification(initiator);

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiator.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(initiator);

			mockMessageRepository
				.Setup(repo => repo.GetMessagesChunkAsync(0, 20, It.IsAny<ISpecification<Message>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(messages);

			var request = new GetMessagesQuery(initiator.Id, 0, 20);
			var handler = new GetMessagesQueryHandler(mockUserRepository.Object, mockMessageRepository.Object);

			var response = await handler.Handle(request, CancellationToken.None);

			Assert.Multiple(() =>
			{
				Assert.That(response, Has.Count.EqualTo(2));
			});
		}

		[Test]
		public void GetMessagesQuery_UserNotFound()
		{
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			var request = new GetMessagesQuery(initiator.Id, 0, 20);
			var handler = new GetMessagesQueryHandler(mockUserRepository.Object, mockMessageRepository.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("User with specified ID doesn't exist."));
		}
	}
}

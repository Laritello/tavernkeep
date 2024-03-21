using Moq;
using Tavernkeep.Application.UseCases.Chat.Commands.DeleteMessage;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

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

            mockMessageRepository
                .Setup(repo => repo.FindAsync(message.Id, default!))
                .ReturnsAsync(message);

            var request = new DeleteMessageCommand(message.Id);
            var handler = new DeleteMessageCommandHandler(mockMessageRepository.Object);

            await handler.Handle(request, CancellationToken.None);
        }

        [Test]
        public void DeleteMessageCommand_MessageNotFound()
        {
            var mockMessageRepository = new Mock<IMessageRepository>();

            var request = new DeleteMessageCommand(message.Id);
            var handler = new DeleteMessageCommandHandler(mockMessageRepository.Object);

            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Message not found."));
        }
    }
}

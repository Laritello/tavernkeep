using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Roll.Commands.RollCustomDice;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;

namespace Tavernkepp.Application.Tests.UseCases.Roll.Commands
{
	public class RollCustomDiceCommandTests
	{
		private readonly string rollExpression = "D20 + 5";
		private readonly int rollResult = 15;
		private readonly User initiator;

		public RollCustomDiceCommandTests()
		{
			initiator = new(string.Empty, string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid(),
			};
		}

		[Test]
		public async Task RollCustomDiceCommand_Success()
		{
			var mockDiceService = new Mock<IDiceService>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.GetDetailsAsync(initiator.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(initiator);

			mockDiceService
				.Setup(service => service.Roll(rollExpression))
				.Returns(new d20Tek.DiceNotation.Results.DiceResult()
				{
					Value = rollResult
				});

			var request = new RollCustomDiceCommand(initiator.Id, RollType.Public, rollExpression);
			var handler = new RollCustomDiceCommandHandler(mockDiceService.Object, mockUserRepository.Object, mockMessageRepository.Object, mockNotificationService.Object);

			var result = await handler.Handle(request, CancellationToken.None);

			Assert.That(result.Result.Value, Is.EqualTo(rollResult));
		}

		[Test]
		public void RollCustomDiceCommand_InitiatorNotFound()
		{
			var mockDiceService = new Mock<IDiceService>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockDiceService
				.Setup(service => service.Roll(rollExpression))
				.Returns(new d20Tek.DiceNotation.Results.DiceResult()
				{
					Value = rollResult
				});

			var request = new RollCustomDiceCommand(initiator.Id, RollType.Public, rollExpression);
			var handler = new RollCustomDiceCommandHandler(mockDiceService.Object, mockUserRepository.Object, mockMessageRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Initiator with specified ID doesn't exist."));
		}
	}
}

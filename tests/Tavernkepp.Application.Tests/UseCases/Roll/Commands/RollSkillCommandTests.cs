using d20Tek.DiceNotation.Results;
using Moq;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.UseCases.Roll.Commands.RollSkill;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;

namespace Tavernkepp.Application.Tests.UseCases.Roll.Commands
{
	public class RollSkillCommandTests
	{
		private readonly SkillType skill = SkillType.Arcana;
		private readonly int rollResult = 15;
		private readonly User initiator;
		private readonly Character character;

		public RollSkillCommandTests()
		{
			initiator = new(string.Empty, string.Empty, UserRole.Player)
			{
				Id = Guid.NewGuid(),
			};

			character = new()
			{
				Id = Guid.NewGuid(),
				Owner = initiator,
			};
		}

		[Test]
		public async Task RollSkillCommand_Success()
		{
			var mockDiceService = new Mock<IDiceService>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiator.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(initiator);

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			mockDiceService
				.Setup(service => service.Roll(character.GetSkill(skill).Bonus, false))
				.Returns(new DiceResult()
				{
					Value = rollResult
				});

			var request = new RollSkillCommand(initiator.Id, character.Id, skill, RollType.Public);
			var handler = new RollSkillCommandHandler(mockDiceService.Object, mockUserRepository.Object, mockCharacterRepository.Object, mockMessageRepository.Object, mockNotificationService.Object);

			var result = await handler.Handle(request, CancellationToken.None);
			Assert.Multiple(() =>
			{
				Assert.That(result.Skill.Type, Is.EqualTo(skill));
				Assert.That(result.Skill.Bonus, Is.EqualTo(character.GetSkill(skill).Bonus));
				Assert.That(result.Result.Value, Is.EqualTo(rollResult));
			});
		}

		[Test]
		public void RollSkillCommand_InitiatorNotFound()
		{
			var mockDiceService = new Mock<IDiceService>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockDiceService
				.Setup(service => service.Roll(character.Arcana.Bonus, false))
				.Returns(new DiceResult()
				{
					Value = rollResult
				});

			mockCharacterRepository
				.Setup(repo => repo.GetFullCharacterAsync(character.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(character);

			var request = new RollSkillCommand(initiator.Id, character.Id, SkillType.Arcana, RollType.Public);
			var handler = new RollSkillCommandHandler(mockDiceService.Object, mockUserRepository.Object, mockCharacterRepository.Object, mockMessageRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Initiator with specified ID doesn't exist."));
		}

		[Test]
		public void RollSkillCommand_CharacterNotFound()
		{
			var mockDiceService = new Mock<IDiceService>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockCharacterRepository = new Mock<ICharacterRepository>();
			var mockMessageRepository = new Mock<IMessageRepository>();
			var mockNotificationService = new Mock<INotificationService>();

			mockUserRepository
				.Setup(repo => repo.FindAsync(initiator.Id, It.IsAny<ISpecification<User>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(initiator);

			mockDiceService
				.Setup(service => service.Roll(character.Arcana.Bonus, false))
				.Returns(new DiceResult()
				{
					Value = rollResult
				});

			var request = new RollSkillCommand(initiator.Id, character.Id, SkillType.Arcana, RollType.Public);
			var handler = new RollSkillCommandHandler(mockDiceService.Object, mockUserRepository.Object, mockCharacterRepository.Object, mockMessageRepository.Object, mockNotificationService.Object);

			Assert.ThatAsync(async () => await handler.Handle(request, CancellationToken.None),
				Throws.TypeOf<BusinessLogicException>()
				.With.Message.EqualTo("Character with specified ID doesn't exist."));
		}
	}
}

using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Notifications;

namespace Tavernkeep.Application.Actions.Characters.Commands.EditAbility
{
    public class EditAbilityCommandHandler(
        IUserRepository userRepository,
        ICharacterRepository characterRepository,
        IHubContext<CharacterHub, ICharacterHub> context
        ) : IRequestHandler<EditAbilityCommand, Ability>
    {
        public async Task<Ability> Handle(EditAbilityCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacter(request.CharacterId)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            // TODO: Throw 403 - forbidden
            if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
                throw new BusinessLogicException("User doesn't have the rights to change other's characters");

            var ability = character.GetAbility(request.Type);
            ability.Score = request.Score;

            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);

            await context.Clients.All.OnAbilityEdited(new AbilityEditedNotification()
            {
                CharacterId = character.Id,
                Type = ability.Type,
                Score = ability.Score
            });

            return ability;
        }
    }
}

using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Commands.ModifyHealth
{
    public class ModifyHealthCommandHandler(IUserRepository userRepository, ICharacterRepository characterRepository) : IRequestHandler<ModifyHealthCommand, Health>
    {
        public async Task<Health> Handle(ModifyHealthCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacterAsync(request.CharacterId)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
                throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

            // If we deal damage to the character that has temporary hp
            // they must be removed before the main heath.

            if (request.Change > 0)
                ApplyHeal(character.Health, request.Change);
            else
                ApplyDamage(character.Health, Math.Abs(request.Change));

            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);

            return character.Health;
        }

        private static void ApplyHeal(Health health, int heal)
        {
            health.Current = Math.Clamp(health.Current + heal, 0, health.Max);
        }

        private static void ApplyDamage(Health health, int damage)
        {
            var leftOverChange = damage;

            if (health.Temporary > 0)
            {
                leftOverChange = Math.Max(0, damage - health.Temporary);
                health.Temporary = Math.Max(0, health.Temporary - damage);
            }

            if (leftOverChange > 0)
            {
                health.Current = Math.Clamp(health.Current - leftOverChange, 0, health.Max);
            }
        }
    }
}

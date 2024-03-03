using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Lores.Commands.EditLore
{
    public class EditLoreCommand : IRequest<Lore>
    {
        public Guid InitiatorId { get; set; }
        public Guid CharacterId { get; set; }
        public string Topic { get; set; }
        public Proficiency Proficiency { get; set; }

        public EditLoreCommand(Guid initiatorId, Guid characterId, string topic, Proficiency proficiency)
        {
            InitiatorId = initiatorId;
            CharacterId = characterId;
            Topic = topic;
            Proficiency = proficiency;
        }
    }
}

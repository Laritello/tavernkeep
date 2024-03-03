using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public UserDto Owner { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Health Health { get; set; } = default!;
        public Dictionary<AbilityType, Ability> Abilities { get; set; } = default!;
        public Dictionary<SkillType, Skill> Skills { get; set; } = default!;
        public List<Lore> Lores { get; set; } = default!;
    }
}

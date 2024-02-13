using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Owner).WithMany(u => u.Characters).IsRequired();

            builder.OwnsJson(c => c.Strength);
            builder.OwnsJson(c => c.Dexterity);
            builder.OwnsJson(c => c.Intelligence);
            builder.OwnsJson(c => c.Constitution);
            builder.OwnsJson(c => c.Wisdom);
            builder.OwnsJson(c => c.Charisma);

            builder.OwnsJson(c => c.Acrobatics);
            builder.OwnsJson(c => c.Arcana);
            builder.OwnsJson(c => c.Athletics);
            builder.OwnsJson(c => c.Crafting);
            builder.OwnsJson(c => c.Deception);
            builder.OwnsJson(c => c.Diplomacy);
            builder.OwnsJson(c => c.Intimidation);
            builder.OwnsJson(c => c.Medicine);
            builder.OwnsJson(c => c.Nature);
            builder.OwnsJson(c => c.Occultism);
            builder.OwnsJson(c => c.Performance);
            builder.OwnsJson(c => c.Religion);
            builder.OwnsJson(c => c.Society);
            builder.OwnsJson(c => c.Stealth);
            builder.OwnsJson(c => c.Survival);
            builder.OwnsJson(c => c.Thievery);
        }
    }
}

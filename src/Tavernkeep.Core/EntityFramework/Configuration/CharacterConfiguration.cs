using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.EntityFramework.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Owner).IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	public class CharacterConfiguration : IEntityTypeConfiguration<Character>
	{
		public void Configure(EntityTypeBuilder<Character> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name).IsRequired().HasDefaultValue("Unknown character");
			builder.Property(c => c.Level).IsRequired().HasDefaultValue(1);

			builder.HasOne(c => c.Class)
				.WithOne(c => c.Owner)
				.HasForeignKey<Class>("OwnerId")
				.IsRequired();

			builder.HasOne(c => c.Ancestry)
				.WithOne(a => a.Owner)
				.HasForeignKey<Ancestry>("OwnerId")
				.IsRequired();

			builder.HasOne(c => c.Owner)
				.WithMany(u => u.Characters)
				.IsRequired();

			builder.HasMany(c => c.Abilities)
				.WithOne(a => a.Owner)
				.IsRequired();

			builder.HasMany(c => c.Skills)
				.WithOne(s => s.Owner)
				.IsRequired();

			builder.OwnsJson(c => c.Health);
			builder.OwnsOne(c => c.Armor, b =>
			{
				b.OwnsOne(a => a.Proficiencies);
				b.OwnsOne(a => a.Equipped);
				b.ToJson();
			});

			builder.OwnsJson(c => c.Walk);
			builder.OwnsJson(c => c.Burrow);
			builder.OwnsJson(c => c.Climb);
			builder.OwnsJson(c => c.Fly);
			builder.OwnsJson(c => c.Swim);

			builder.OwnsMany(c => c.Conditions, b =>
			{
				b.ToJson();
				b.OwnsMany(con => con.Modifiers, b => b.ToJson());
			});
		}
	}
}

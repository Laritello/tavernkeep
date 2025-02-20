using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Contracts.Structures;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Infrastructure.Data.Configuration.Library
{
	public class ConditionInformationConfiguration : IEntityTypeConfiguration<Condition>
	{
		public void Configure(EntityTypeBuilder<Condition> builder)
		{
			builder.HasKey(c => c.Name);
			builder.Property(c => c.Name).IsRequired();
			builder.Property(c => c.Description).IsRequired();
			builder.Property(c => c.HasLevels).IsRequired();

			builder.Property(c => c.Modifiers)
				.HasConversion(
				v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
				v => JsonSerializer.Deserialize<Dictionary<string, Modifier>>(v, JsonSerializerOptions.Default) ?? new Dictionary<string, Modifier>()
			);

			builder.HasMany(c => c.Related)
				.WithMany()
				.UsingEntity(join => join.ToTable("LibraryConditionRelated")); ;
		}
	}
}

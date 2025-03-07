using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Entities.Library.Creatures;

namespace Tavernkeep.Infrastructure.Data.Configuration.Library
{
	public class CreatureConfiguration : IEntityTypeConfiguration<Creature>
	{
		public void Configure(EntityTypeBuilder<Creature> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Blocks)
				.HasConversion(
				v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
				v => JsonSerializer.Deserialize<List<IStatblock>>(v, JsonSerializerOptions.Default) ?? new List<IStatblock>()
			);
		}
	}
}

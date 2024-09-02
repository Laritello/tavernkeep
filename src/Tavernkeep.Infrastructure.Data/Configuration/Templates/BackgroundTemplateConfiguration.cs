using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Infrastructure.Data.Configuration.Templates
{
	public class BackgroundTemplateConfiguration : IEntityTypeConfiguration<BackgroundTemplate>
	{
		public void Configure(EntityTypeBuilder<BackgroundTemplate> builder)
		{
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Name).IsRequired();

			builder.Property(a => a.Attributes)
				.HasConversion(
					v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
					v => JsonSerializer.Deserialize<List<BuildAttribute>>(v, JsonSerializerOptions.Default) ?? new List<BuildAttribute>(),
					new ValueComparer<List<BuildAttribute>>(
						(c1, c2) => c1!.SequenceEqual(c2!),
						c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
						c => c.ToList()));
		}
	}
}

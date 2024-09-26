using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Infrastructure.Data.Configuration.Templates
{
	internal class ClassTemplateConfiguration : IEntityTypeConfiguration<ClassTemplate>
	{
		public void Configure(EntityTypeBuilder<ClassTemplate> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name).IsRequired();
			builder.Property(c => c.HitPoints).IsRequired();

			builder.Property(c => c.Attributes)
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

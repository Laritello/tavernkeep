using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			builder.HasIndex(u => u.Login).IsUnique();

			builder.Property(u => u.Login).IsRequired();
			builder.Property(u => u.Password).IsRequired();

			builder.Property(u => u.Role)
				.IsRequired()
				.HasConversion(new EnumToStringConverter<UserRole>());

			builder.HasOne(u => u.ActiveCharacter);
		}
	}
}

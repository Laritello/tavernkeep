using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Infrastructure.Data.Configuration
{
	public class CreatureConfiguration : IEntityTypeConfiguration<Creature>
	{
		public void Configure(EntityTypeBuilder<Creature> builder)
		{
			builder.HasKey(c => c.Id);

			builder.OwnsOne(c => c.Health, b => b.ToJson());
			
			builder.Property(c => c.Abilities)
				.HasConversion(
					v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default), 
					v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, JsonSerializerOptions.Default) ?? new Dictionary<string, int>()
				);

			builder.Property(c => c.Skills)
				.HasConversion(
					v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
					v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, JsonSerializerOptions.Default) ?? new Dictionary<string, int>()
				);

			builder.Property(c => c.SavingThrows)
				.HasConversion(
					v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
					v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, JsonSerializerOptions.Default) ?? new Dictionary<string, int>()
				);

			builder.Property(c => c.Notes)
				.HasConversion(
					v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
					v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, JsonSerializerOptions.Default) ?? new Dictionary<string, string>()
				);

			builder.OwnsMany(c => c.Senses, b => b.ToJson());
			builder.OwnsMany(c => c.Resistances, b => b.ToJson());
			builder.OwnsMany(c => c.Weaknesses, b => b.ToJson());
			builder.OwnsMany(c => c.Speeds, b => b.ToJson());
		}
	}
}

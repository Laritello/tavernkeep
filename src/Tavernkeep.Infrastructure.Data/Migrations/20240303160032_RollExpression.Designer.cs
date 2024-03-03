﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tavernkeep.Infrastructure.Data.Context;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SessionContext))]
    [Migration("20240303160032_RollExpression")]
    partial class RollExpression
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Tavernkeep.Core.Entities.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Message");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.RollMessage", b =>
                {
                    b.HasBaseType("Tavernkeep.Core.Entities.Messages.Message");

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RollType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Messages");

                    b.HasDiscriminator().HasValue("RollMessage");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.TextMessage", b =>
                {
                    b.HasBaseType("Tavernkeep.Core.Entities.Messages.Message");

                    b.Property<Guid?>("RecipientId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("RecipientId");

                    b.ToTable("Messages");

                    b.HasDiscriminator().HasValue("TextMessage");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Character", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.User", "Owner")
                        .WithMany("Characters")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Tavernkeep.Core.Entities.Health", "Health", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Current")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Max")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Temporary")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Health");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Acrobatics", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Acrobatics");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Arcana", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Arcana");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Athletics", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Athletics");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Ability", "Charisma", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Charisma");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Ability", "Constitution", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Constitution");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Crafting", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Crafting");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Deception", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Deception");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Ability", "Dexterity", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Dexterity");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Diplomacy", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Diplomacy");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Ability", "Intelligence", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Intelligence");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Intimidation", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Intimidation");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Medicine", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Medicine");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Nature", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Nature");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Occultism", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Occultism");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Performance", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Performance");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Religion", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Religion");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Society", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Society");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Stealth", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Stealth");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Ability", "Strength", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Strength");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Survival", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Survival");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Skill", "Thievery", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Thievery");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Ability", "Wisdom", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Characters");

                            b1.ToJson("Wisdom");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.Navigation("Acrobatics")
                        .IsRequired();

                    b.Navigation("Arcana")
                        .IsRequired();

                    b.Navigation("Athletics")
                        .IsRequired();

                    b.Navigation("Charisma")
                        .IsRequired();

                    b.Navigation("Constitution")
                        .IsRequired();

                    b.Navigation("Crafting")
                        .IsRequired();

                    b.Navigation("Deception")
                        .IsRequired();

                    b.Navigation("Dexterity")
                        .IsRequired();

                    b.Navigation("Diplomacy")
                        .IsRequired();

                    b.Navigation("Health")
                        .IsRequired();

                    b.Navigation("Intelligence")
                        .IsRequired();

                    b.Navigation("Intimidation")
                        .IsRequired();

                    b.Navigation("Medicine")
                        .IsRequired();

                    b.Navigation("Nature")
                        .IsRequired();

                    b.Navigation("Occultism")
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Performance")
                        .IsRequired();

                    b.Navigation("Religion")
                        .IsRequired();

                    b.Navigation("Society")
                        .IsRequired();

                    b.Navigation("Stealth")
                        .IsRequired();

                    b.Navigation("Strength")
                        .IsRequired();

                    b.Navigation("Survival")
                        .IsRequired();

                    b.Navigation("Thievery")
                        .IsRequired();

                    b.Navigation("Wisdom")
                        .IsRequired();
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.Message", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.RollMessage", b =>
                {
                    b.OwnsOne("Tavernkeep.Core.Entities.Rolls.RollResult", "Result", b1 =>
                        {
                            b1.Property<Guid>("RollMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Value")
                                .HasColumnType("INTEGER");

                            b1.HasKey("RollMessageId");

                            b1.ToTable("Messages");

                            b1.ToJson("Result");

                            b1.WithOwner()
                                .HasForeignKey("RollMessageId");

                            b1.OwnsMany("Tavernkeep.Core.Entities.Rolls.ThrowResult", "Results", b2 =>
                                {
                                    b2.Property<Guid>("RollResultRollMessageId")
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAddOrUpdate()
                                        .HasColumnType("INTEGER");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("RollResultRollMessageId", "Id");

                                    b2.ToTable("Messages");

                                    b2.WithOwner()
                                        .HasForeignKey("RollResultRollMessageId");
                                });

                            b1.Navigation("Results");
                        });

                    b.Navigation("Result")
                        .IsRequired();
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.TextMessage", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.User", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}

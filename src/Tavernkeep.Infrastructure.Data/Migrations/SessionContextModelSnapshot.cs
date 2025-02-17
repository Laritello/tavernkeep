﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tavernkeep.Infrastructure.Data.Context;

#nullable disable

namespace Tavernkeep.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SessionContext))]
    partial class SessionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");

                    b.HasDiscriminator().HasValue("Message");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("HeroPoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Unknown character");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Conditions.ConditionTemplate", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasLevels")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("Conditions");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Portrait", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Portrait");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Ability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("CharacterAbility");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Ancestry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CharacterAncestry");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Class", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("HealthPerLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CharacterClass");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Health", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Current")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Temporary")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CharacterHealth");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AbilityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Pinned")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Proficiency")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AbilityId");

                    b.HasIndex("OwnerId");

                    b.ToTable("CharacterSkill");
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

                    b.Property<Guid?>("ActiveCharacterId")
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

                    b.HasIndex("ActiveCharacterId");

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

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.SkillRollMessage", b =>
                {
                    b.HasBaseType("Tavernkeep.Core.Entities.Messages.RollMessage");

                    b.ToTable("Messages");

                    b.HasDiscriminator().HasValue("SkillRollMessage");
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

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Character", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.User", "Owner")
                        .WithMany("Characters")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Tavernkeep.Core.Entities.Pathfinder.Conditions.Condition", "Conditions", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("HasLevels")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Level")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("CharacterId", "__synthesizedOrdinal");

                            b1.ToTable("Character");

                            b1.ToJson("Conditions");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");

                            b1.OwnsMany("Tavernkeep.Core.Entities.Pathfinder.Modifiers.Modifier", "Modifiers", b2 =>
                                {
                                    b2.Property<Guid>("ConditionCharacterId")
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Condition__synthesizedOrdinal")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAddOrUpdate()
                                        .HasColumnType("INTEGER");

                                    b2.Property<bool>("IsBonus")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Scaling")
                                        .HasColumnType("INTEGER");

                                    b2.PrimitiveCollection<string>("Targets")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Type")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("ConditionCharacterId", "Condition__synthesizedOrdinal", "__synthesizedOrdinal");

                                    b2.ToTable("Character");

                                    b2.ToJson("Modifiers");

                                    b2.WithOwner()
                                        .HasForeignKey("ConditionCharacterId", "Condition__synthesizedOrdinal");
                                });

                            b1.Navigation("Modifiers");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Speed", "Burrow", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("Active")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Base")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Character");

                            b1.ToJson("Burrow");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Speed", "Climb", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("Active")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Base")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Character");

                            b1.ToJson("Climb");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Speed", "Fly", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("Active")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Base")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Character");

                            b1.ToJson("Fly");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Speed", "Swim", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("Active")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Base")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Character");

                            b1.ToJson("Swim");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Speed", "Walk", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("Active")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Base")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Character");

                            b1.ToJson("Walk");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.Navigation("Owner");
                        });

                    b.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Armor", "Armor", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("TEXT");

                            b1.HasKey("OwnerId");

                            b1.ToTable("Character");

                            b1.ToJson("Armor");

                            b1.WithOwner("Owner")
                                .HasForeignKey("OwnerId");

                            b1.OwnsOne("Tavernkeep.Core.Contracts.Structures.EquippedArmor", "Equipped", b2 =>
                                {
                                    b2.Property<Guid>("ArmorOwnerId")
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Bonus")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("DexterityCap")
                                        .HasColumnType("INTEGER");

                                    b2.Property<bool>("HasDexterityCap")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Type")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("ArmorOwnerId");

                                    b2.ToTable("Character");

                                    b2.WithOwner()
                                        .HasForeignKey("ArmorOwnerId");
                                });

                            b1.OwnsOne("Tavernkeep.Core.Entities.Pathfinder.Properties.ArmorProficiencies", "Proficiencies", b2 =>
                                {
                                    b2.Property<Guid>("ArmorOwnerId")
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Heavy")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Light")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Medium")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Unarmored")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("ArmorOwnerId");

                                    b2.ToTable("Character");

                                    b2.WithOwner()
                                        .HasForeignKey("ArmorOwnerId");
                                });

                            b1.Navigation("Equipped")
                                .IsRequired();

                            b1.Navigation("Owner");

                            b1.Navigation("Proficiencies")
                                .IsRequired();
                        });

                    b.Navigation("Armor")
                        .IsRequired();

                    b.Navigation("Burrow")
                        .IsRequired();

                    b.Navigation("Climb")
                        .IsRequired();

                    b.Navigation("Conditions");

                    b.Navigation("Fly")
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Swim")
                        .IsRequired();

                    b.Navigation("Walk")
                        .IsRequired();
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Conditions.ConditionTemplate", b =>
                {
                    b.OwnsMany("Tavernkeep.Core.Entities.Pathfinder.Modifiers.Modifier", "Modifiers", b1 =>
                        {
                            b1.Property<string>("ConditionTemplateName")
                                .HasColumnType("TEXT");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsBonus")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Scaling")
                                .HasColumnType("INTEGER");

                            b1.PrimitiveCollection<string>("Targets")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Value")
                                .HasColumnType("INTEGER");

                            b1.HasKey("ConditionTemplateName", "__synthesizedOrdinal");

                            b1.ToTable("Conditions");

                            b1.ToJson("Modifiers");

                            b1.WithOwner()
                                .HasForeignKey("ConditionTemplateName");
                        });

                    b.OwnsMany("Tavernkeep.Core.Entities.Pathfinder.Conditions.Condition", "Related", b1 =>
                        {
                            b1.Property<string>("ConditionTemplateName")
                                .HasColumnType("TEXT");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("HasLevels")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Level")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("ConditionTemplateName", "__synthesizedOrdinal");

                            b1.ToTable("Conditions");

                            b1.ToJson("Related");

                            b1.WithOwner()
                                .HasForeignKey("ConditionTemplateName");

                            b1.OwnsMany("Tavernkeep.Core.Entities.Pathfinder.Modifiers.Modifier", "Modifiers", b2 =>
                                {
                                    b2.Property<string>("ConditionTemplateName")
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Condition__synthesizedOrdinal")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAddOrUpdate()
                                        .HasColumnType("INTEGER");

                                    b2.Property<bool>("IsBonus")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Scaling")
                                        .HasColumnType("INTEGER");

                                    b2.PrimitiveCollection<string>("Targets")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Type")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("ConditionTemplateName", "Condition__synthesizedOrdinal", "__synthesizedOrdinal");

                                    b2.ToTable("Conditions");

                                    b2.ToJson("Modifiers");

                                    b2.WithOwner()
                                        .HasForeignKey("ConditionTemplateName", "Condition__synthesizedOrdinal");
                                });

                            b1.Navigation("Modifiers");
                        });

                    b.Navigation("Modifiers");

                    b.Navigation("Related");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Portrait", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "Owner")
                        .WithOne("Portrait")
                        .HasForeignKey("Tavernkeep.Core.Entities.Pathfinder.Portrait", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Ability", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "Owner")
                        .WithMany("Abilities")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Ancestry", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "Owner")
                        .WithOne("Ancestry")
                        .HasForeignKey("Tavernkeep.Core.Entities.Pathfinder.Properties.Ancestry", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Class", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "Owner")
                        .WithOne("Class")
                        .HasForeignKey("Tavernkeep.Core.Entities.Pathfinder.Properties.Class", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Health", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "Owner")
                        .WithOne("Health")
                        .HasForeignKey("Tavernkeep.Core.Entities.Pathfinder.Properties.Health", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Properties.Skill", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Properties.Ability", "Ability")
                        .WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "Owner")
                        .WithMany("Skills")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.User", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.Pathfinder.Character", "ActiveCharacter")
                        .WithMany()
                        .HasForeignKey("ActiveCharacterId");

                    b.Navigation("ActiveCharacter");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.RollMessage", b =>
                {
                    b.OwnsOne("Tavernkeep.Core.Entities.Rolls.RollResult", "Result", b1 =>
                        {
                            b1.Property<Guid>("RollMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Modifier")
                                .HasColumnType("INTEGER");

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

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAddOrUpdate()
                                        .HasColumnType("INTEGER");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("RollResultRollMessageId", "__synthesizedOrdinal");

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

            modelBuilder.Entity("Tavernkeep.Core.Entities.Messages.SkillRollMessage", b =>
                {
                    b.OwnsOne("Tavernkeep.Core.Entities.Snapshots.SkillSnapshot", "Skill", b1 =>
                        {
                            b1.Property<Guid>("SkillRollMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Bonus")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("SkillRollMessageId");

                            b1.ToTable("Messages");

                            b1.ToJson("Skill");

                            b1.WithOwner()
                                .HasForeignKey("SkillRollMessageId");
                        });

                    b.Navigation("Skill")
                        .IsRequired();
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Pathfinder.Character", b =>
                {
                    b.Navigation("Abilities");

                    b.Navigation("Ancestry")
                        .IsRequired();

                    b.Navigation("Class")
                        .IsRequired();

                    b.Navigation("Health")
                        .IsRequired();

                    b.Navigation("Portrait");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.User", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}

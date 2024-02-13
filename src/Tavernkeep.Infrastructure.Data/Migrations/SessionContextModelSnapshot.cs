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
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Tavernkeep.Core.Entities.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
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

            modelBuilder.Entity("Tavernkeep.Core.Entities.Character", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.User", "Owner")
                        .WithMany("Characters")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Acrobatics", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Acrobatics");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Arcana", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Arcana");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Athletics", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Athletics");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Ability", "Charisma", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Charisma");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Ability", "Constitution", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Constitution");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Crafting", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Crafting");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Deception", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Deception");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Ability", "Dexterity", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Dexterity");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Diplomacy", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Diplomacy");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Ability", "Intelligence", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Intelligence");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Intimidation", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Intimidation");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Medicine", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Medicine");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Nature", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Nature");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Occultism", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Occultism");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Performance", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Performance");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Religion", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Religion");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Society", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Society");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Stealth", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Stealth");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Ability", "Strength", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Strength");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Survival", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Survival");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Skill", "Thievery", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Proficiency")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Thievery");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("Tavernkeep.Core.Contracts.Character.Ability", "Wisdom", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Score")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.ToJson("Wisdom");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
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

            modelBuilder.Entity("Tavernkeep.Core.Entities.Message", b =>
                {
                    b.HasOne("Tavernkeep.Core.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Tavernkeep.Core.Entities.User", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourOfHeroes.Data;

#nullable disable

namespace TourOfHeroes.Migrations
{
    [DbContext(typeof(DbHeroContext))]
    [Migration("20230329172621_atualizacao-list-heromodel")]
    partial class atualizacaolistheromodel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("HeroModelSkillModel", b =>
                {
                    b.Property<int>("HeroesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HeroesId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("HeroModelSkillModel");
                });

            modelBuilder.Entity("TourOfHeroes.Models.HeroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("TourOfHeroes.Models.HeroSkillModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("HeroId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HeroId");

                    b.HasIndex("SkillId");

                    b.ToTable("HeroSkills");
                });

            modelBuilder.Entity("TourOfHeroes.Models.SkillModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Damage")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("HeroModelSkillModel", b =>
                {
                    b.HasOne("TourOfHeroes.Models.HeroModel", null)
                        .WithMany()
                        .HasForeignKey("HeroesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourOfHeroes.Models.SkillModel", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourOfHeroes.Models.HeroSkillModel", b =>
                {
                    b.HasOne("TourOfHeroes.Models.HeroModel", "Hero")
                        .WithMany("HeroSkills")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourOfHeroes.Models.SkillModel", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("TourOfHeroes.Models.HeroModel", b =>
                {
                    b.Navigation("HeroSkills");
                });
#pragma warning restore 612, 618
        }
    }
}

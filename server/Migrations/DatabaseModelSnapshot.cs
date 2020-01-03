﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using atlantis.Persistence;

namespace atlantis.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("atlantis.Persistence.DbEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("FactionId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FactionId");

                    b.HasIndex("GameId");

                    b.HasIndex("TurnId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("atlantis.Persistence.DbFaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Own")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("TurnId");

                    b.ToTable("Factions");
                });

            modelBuilder.Entity("atlantis.Persistence.DbGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EngineVersion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Memory")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PlayerFactionNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RulesetName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RulesetVersion")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("atlantis.Persistence.DbRegion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Memory")
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Terrain")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Z")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("TurnId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("atlantis.Persistence.DbStructure", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Memory")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RegionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sequence")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("RegionId");

                    b.HasIndex("TurnId");

                    b.ToTable("Structures");
                });

            modelBuilder.Entity("atlantis.Persistence.DbTurn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Memory")
                        .HasColumnType("TEXT");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Turns");
                });

            modelBuilder.Entity("atlantis.Persistence.DbUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FactionNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Memory")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Orders")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Own")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RegionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sequence")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("StrcutureId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("RegionId");

                    b.HasIndex("StrcutureId");

                    b.HasIndex("TurnId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("atlantis.Persistence.DbEvent", b =>
                {
                    b.HasOne("atlantis.Persistence.DbFaction", "Faction")
                        .WithMany("Events")
                        .HasForeignKey("FactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Events")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("atlantis.Persistence.DbFaction", b =>
                {
                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Factions")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("atlantis.Persistence.DbRegion", b =>
                {
                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Regions")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("atlantis.Persistence.DbStructure", b =>
                {
                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbRegion", "Region")
                        .WithMany("Structures")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Structures")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("atlantis.Persistence.DbTurn", b =>
                {
                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany("Turns")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("atlantis.Persistence.DbUnit", b =>
                {
                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbRegion", "Region")
                        .WithMany("Units")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbStructure", "Structure")
                        .WithMany("Units")
                        .HasForeignKey("StrcutureId");

                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Units")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

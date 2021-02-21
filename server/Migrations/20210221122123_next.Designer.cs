﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using atlantis.Persistence;

namespace atlantis.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20210221122123_next")]
    partial class next
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("atlantis.Persistence.DbGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EngineVersion")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastTurnNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerFactionName")
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

                    b.Property<int>("Entertainment")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Population")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Race")
                        .HasColumnType("TEXT");

                    b.Property<int>("Tax")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Terrain")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalWages")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdatedAtTurn")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Wages")
                        .HasColumnType("REAL");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Z")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TurnId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("atlantis.Persistence.DbReport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FactionName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("FactionNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("TurnId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("TurnId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("atlantis.Persistence.DbTurn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

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

            modelBuilder.Entity("atlantis.Persistence.DbRegion", b =>
                {
                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Regions")
                        .HasForeignKey("TurnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("atlantis.Persistence.DbItem", "Products", b1 =>
                        {
                            b1.Property<long>("RegionId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Code")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Count")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("RegionId", "Code");

                            b1.ToTable("Regions_Products");

                            b1.WithOwner()
                                .HasForeignKey("RegionId");
                        });

                    b.OwnsMany("atlantis.Persistence.DbRegionExit", "Exits", b1 =>
                        {
                            b1.Property<long>("RegionId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("RegionUID")
                                .HasColumnType("TEXT");

                            b1.HasKey("RegionId", "RegionUID");

                            b1.ToTable("Regions_Exits");

                            b1.WithOwner()
                                .HasForeignKey("RegionId");
                        });

                    b.OwnsMany("atlantis.Persistence.DbTradableItem", "ForSale", b1 =>
                        {
                            b1.Property<long>("RegionId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Code")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Count")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Price")
                                .HasColumnType("INTEGER");

                            b1.HasKey("RegionId", "Code");

                            b1.ToTable("Regions_ForSale");

                            b1.WithOwner()
                                .HasForeignKey("RegionId");
                        });

                    b.OwnsMany("atlantis.Persistence.DbTradableItem", "Wanted", b1 =>
                        {
                            b1.Property<long>("RegionId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Code")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Count")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Price")
                                .HasColumnType("INTEGER");

                            b1.HasKey("RegionId", "Code");

                            b1.ToTable("Regions_Wanted");

                            b1.WithOwner()
                                .HasForeignKey("RegionId");
                        });
                });

            modelBuilder.Entity("atlantis.Persistence.DbReport", b =>
                {
                    b.HasOne("atlantis.Persistence.DbGame", "Game")
                        .WithMany("Reports")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("atlantis.Persistence.DbTurn", "Turn")
                        .WithMany("Reports")
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
#pragma warning restore 612, 618
        }
    }
}
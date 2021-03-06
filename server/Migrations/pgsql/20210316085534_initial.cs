﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace advisor.Migrations.pgsql
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    RemoteGameOptions = table.Column<string>(type: "text", nullable: true),
                    EngineVersion = table.Column<string>(type: "text", nullable: true),
                    RulesetName = table.Column<string>(type: "text", nullable: true),
                    RulesetVersion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    Algorithm = table.Column<string>(type: "text", nullable: false),
                    Digest = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Universities_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    FactionNumber = table.Column<int>(type: "integer", nullable: true),
                    FactionName = table.Column<string>(type: "text", nullable: true),
                    LastTurnNumber = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Role",
                columns: table => new
                {
                    Role = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Role", x => new { x.UserId, x.Role });
                    table.ForeignKey(
                        name: "FK_Users_Role_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turns_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniversityMemberships",
                columns: table => new
                {
                    UniversityId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityMemberships", x => new { x.PlayerId, x.UniversityId });
                    table.ForeignKey(
                        name: "FK_UniversityMemberships_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UniversityMemberships_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factions_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Z = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAtTurn = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "text", nullable: false),
                    Terrain = table.Column<string>(type: "text", nullable: false),
                    Settlement_Name = table.Column<string>(type: "text", nullable: true),
                    Settlement_Size = table.Column<string>(type: "text", nullable: true),
                    Population = table.Column<int>(type: "integer", nullable: false),
                    Race = table.Column<string>(type: "text", nullable: true),
                    Entertainment = table.Column<int>(type: "integer", nullable: false),
                    Tax = table.Column<int>(type: "integer", nullable: false),
                    Wages = table.Column<double>(type: "double precision", nullable: false),
                    TotalWages = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    FactionNumber = table.Column<int>(type: "integer", nullable: false),
                    FactionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    Json = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    FactionId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Factions_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions_Exits",
                columns: table => new
                {
                    Direction = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Z = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "text", nullable: false),
                    Terrain = table.Column<string>(type: "text", nullable: false),
                    Settlement_Name = table.Column<string>(type: "text", nullable: true),
                    Settlement_Size = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions_Exits", x => new { x.RegionId, x.Direction });
                    table.ForeignKey(
                        name: "FK_Regions_Exits_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions_ForSale",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions_ForSale", x => new { x.RegionId, x.Code });
                    table.ForeignKey(
                        name: "FK_Regions_ForSale_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions_Products",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions_Products", x => new { x.RegionId, x.Code });
                    table.ForeignKey(
                        name: "FK_Regions_Products_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions_Wanted",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions_Wanted", x => new { x.RegionId, x.Code });
                    table.ForeignKey(
                        name: "FK_Regions_Wanted_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Structures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Z = table.Column<int>(type: "integer", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Flags = table.Column<string>(type: "text", nullable: true),
                    SailDirections = table.Column<string>(type: "text", nullable: true),
                    Speed = table.Column<int>(type: "integer", nullable: true),
                    Needs = table.Column<int>(type: "integer", nullable: true),
                    Load_Used = table.Column<int>(type: "integer", nullable: true),
                    Load_Max = table.Column<int>(type: "integer", nullable: true),
                    Sailors_Current = table.Column<int>(type: "integer", nullable: true),
                    Sailors_Required = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structures_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Structures_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Structures_Contents",
                columns: table => new
                {
                    Type = table.Column<string>(type: "text", nullable: false),
                    StructureId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structures_Contents", x => new { x.StructureId, x.Type });
                    table.ForeignKey(
                        name: "FK_Structures_Contents_Structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    StrcutureId = table.Column<long>(type: "bigint", nullable: true),
                    FactionId = table.Column<long>(type: "bigint", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    OnGuard = table.Column<bool>(type: "boolean", nullable: false),
                    Flags = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: true),
                    Capacity_Flying = table.Column<int>(type: "integer", nullable: true),
                    Capacity_Riding = table.Column<int>(type: "integer", nullable: true),
                    Capacity_Walking = table.Column<int>(type: "integer", nullable: true),
                    Capacity_Swimming = table.Column<int>(type: "integer", nullable: true),
                    ReadyItem_Code = table.Column<string>(type: "text", nullable: true),
                    ReadyItem_Amount = table.Column<int>(type: "integer", nullable: true),
                    CombatSpell_Code = table.Column<string>(type: "text", nullable: true),
                    CombatSpell_Level = table.Column<int>(type: "integer", nullable: true),
                    CombatSpell_Days = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Factions_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Units_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Structures_StrcutureId",
                        column: x => x.StrcutureId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Units_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniversityId = table.Column<long>(type: "bigint", nullable: false),
                    TurnId = table.Column<long>(type: "bigint", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    Target_Code = table.Column<string>(type: "text", nullable: true),
                    Target_Level = table.Column<int>(type: "integer", nullable: true),
                    Target_Days = table.Column<int>(type: "integer", nullable: true),
                    Study = table.Column<string>(type: "text", nullable: true),
                    Teach = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPlans_Turns_TurnId",
                        column: x => x.TurnId,
                        principalTable: "Turns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyPlans_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyPlans_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit_CanStudy",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    Days = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit_CanStudy", x => new { x.UnitId, x.Code });
                    table.ForeignKey(
                        name: "FK_Unit_CanStudy_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit_Items",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit_Items", x => new { x.UnitId, x.Code });
                    table.ForeignKey(
                        name: "FK_Unit_Items_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit_Skills",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    Days = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit_Skills", x => new { x.UnitId, x.Code });
                    table.ForeignKey(
                        name: "FK_Unit_Skills_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_FactionId",
                table: "Events",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TurnId",
                table: "Events",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_Factions_TurnId",
                table: "Factions",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_TurnId",
                table: "Regions",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PlayerId",
                table: "Reports",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TurnId",
                table: "Reports",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_RegionId",
                table: "Structures",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_TurnId",
                table: "Structures",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_TurnId",
                table: "StudyPlans",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_UnitId",
                table: "StudyPlans",
                column: "UnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_UniversityId",
                table: "StudyPlans",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_PlayerId",
                table: "Turns",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_FactionId",
                table: "Units",
                column: "FactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_RegionId",
                table: "Units",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_StrcutureId",
                table: "Units",
                column: "StrcutureId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_TurnId",
                table: "Units",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_GameId",
                table: "Universities",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMemberships_PlayerId",
                table: "UniversityMemberships",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMemberships_UniversityId",
                table: "UniversityMemberships",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Regions_Exits");

            migrationBuilder.DropTable(
                name: "Regions_ForSale");

            migrationBuilder.DropTable(
                name: "Regions_Products");

            migrationBuilder.DropTable(
                name: "Regions_Wanted");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Structures_Contents");

            migrationBuilder.DropTable(
                name: "StudyPlans");

            migrationBuilder.DropTable(
                name: "Unit_CanStudy");

            migrationBuilder.DropTable(
                name: "Unit_Items");

            migrationBuilder.DropTable(
                name: "Unit_Skills");

            migrationBuilder.DropTable(
                name: "UniversityMemberships");

            migrationBuilder.DropTable(
                name: "Users_Role");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropTable(
                name: "Factions");

            migrationBuilder.DropTable(
                name: "Structures");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Turns");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

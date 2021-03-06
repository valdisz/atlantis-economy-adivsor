﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace advisor.Migrations.pgsql
{
    public partial class events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemPrice",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Terrain",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Z",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_UnitId",
                table: "Events",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Units_UnitId",
                table: "Events",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Units_UnitId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UnitId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ItemPrice",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Terrain",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Z",
                table: "Events");
        }
    }
}

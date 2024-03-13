using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Improved2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CommunityId",
                table: "Topics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Communities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Communities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Communities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Communities",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Communities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CommunityId",
                table: "Topics",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Communities_CommunityId",
                table: "Topics",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Communities_CommunityId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_CommunityId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Communities");
        }
    }
}

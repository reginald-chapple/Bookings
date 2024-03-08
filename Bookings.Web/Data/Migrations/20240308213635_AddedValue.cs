using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Value",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Value", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserValues",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ValueId = table.Column<long>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserValues", x => new { x.ValueId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserValues_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserValues_Value_ValueId",
                        column: x => x.ValueId,
                        principalTable: "Value",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserValues_UserId",
                table: "UserValues",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserValues");

            migrationBuilder.DropTable(
                name: "Value");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationship1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserValues_Value_ValueId",
                table: "UserValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Value",
                table: "Value");

            migrationBuilder.RenameTable(
                name: "Value",
                newName: "Values");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Values",
                table: "Values",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserValues_Values_ValueId",
                table: "UserValues",
                column: "ValueId",
                principalTable: "Values",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserValues_Values_ValueId",
                table: "UserValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Values",
                table: "Values");

            migrationBuilder.RenameTable(
                name: "Values",
                newName: "Value");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Value",
                table: "Value",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserValues_Value_ValueId",
                table: "UserValues",
                column: "ValueId",
                principalTable: "Value",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

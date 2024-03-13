using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Improved3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Causes_CauseId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Communities_CauseId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "CauseId",
                table: "Communities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CauseId",
                table: "Communities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_CauseId",
                table: "Communities",
                column: "CauseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Causes_CauseId",
                table: "Communities",
                column: "CauseId",
                principalTable: "Causes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

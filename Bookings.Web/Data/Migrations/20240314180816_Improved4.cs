using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Improved4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Communities");

            migrationBuilder.AddColumn<long>(
                name: "CampaignId",
                table: "Communities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_CampaignId",
                table: "Communities",
                column: "CampaignId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Campaigns_CampaignId",
                table: "Communities",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Campaigns_CampaignId",
                table: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Communities_CampaignId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Communities");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Communities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Communities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MC1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "Meetings",
                newName: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CampaignId",
                table: "Meetings",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Campaigns_CampaignId",
                table: "Meetings",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Campaigns_CampaignId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CampaignId",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "Meetings",
                newName: "EntityType");

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Meetings",
                type: "INTEGER",
                nullable: true);
        }
    }
}

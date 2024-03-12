using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Optimized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_Milestones_MilestoneId",
                table: "ActionItems");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropIndex(
                name: "IX_ActionItems_MilestoneId",
                table: "ActionItems");

            migrationBuilder.RenameColumn(
                name: "MilestoneId",
                table: "ActionItems",
                newName: "Type");

            migrationBuilder.AddColumn<long>(
                name: "CampaignId",
                table: "ActionItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "ActionItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "ActionItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "ActionItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageComplete",
                table: "ActionItems",
                type: "TEXT",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_CampaignId",
                table: "ActionItems",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_ParentId",
                table: "ActionItems",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_ActionItems_ParentId",
                table: "ActionItems",
                column: "ParentId",
                principalTable: "ActionItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_Campaigns_CampaignId",
                table: "ActionItems",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_ActionItems_ParentId",
                table: "ActionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_Campaigns_CampaignId",
                table: "ActionItems");

            migrationBuilder.DropIndex(
                name: "IX_ActionItems_CampaignId",
                table: "ActionItems");

            migrationBuilder.DropIndex(
                name: "IX_ActionItems_ParentId",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "PercentageComplete",
                table: "ActionItems");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ActionItems",
                newName: "MilestoneId");

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CampaignId = table.Column<long>(type: "INTEGER", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Deadline = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    PercentageComplete = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestones_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_MilestoneId",
                table: "ActionItems",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_CampaignId",
                table: "Milestones",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_Milestones_MilestoneId",
                table: "ActionItems",
                column: "MilestoneId",
                principalTable: "Milestones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

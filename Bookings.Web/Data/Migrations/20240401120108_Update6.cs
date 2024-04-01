using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpenForMembership",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "TeamRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpenForMembership",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "TeamRequests");
        }
    }
}

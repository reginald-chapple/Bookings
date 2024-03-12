﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Improved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Campaigns",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Campaigns");
        }
    }
}

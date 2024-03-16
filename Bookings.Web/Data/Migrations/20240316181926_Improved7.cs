using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Improved7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_AspNetUsers_SeekerId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_AspNetUsers_TargetId",
                table: "Relationships");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "MatchProfiles");

            migrationBuilder.DropTable(
                name: "SkinComplexions");

            migrationBuilder.DropIndex(
                name: "IX_Relationships_SeekerId",
                table: "Relationships");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "Relationships",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SeekerId",
                table: "Relationships",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "Relationships",
                newName: "IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_TargetId",
                table: "Relationships",
                newName: "IX_Relationships_UserId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Relationships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Relationships",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Relationships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_AspNetUsers_UserId",
                table: "Relationships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_AspNetUsers_UserId",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Relationships");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Relationships",
                newName: "TargetId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Relationships",
                newName: "SeekerId");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Relationships",
                newName: "IsPrivate");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_UserId",
                table: "Relationships",
                newName: "IX_Relationships_TargetId");

            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinComplexions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HexCode = table.Column<string>(type: "TEXT", nullable: false),
                    ScalePosition = table.Column<string>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinComplexions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComplexionId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    BodyType = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EyeColor = table.Column<int>(type: "INTEGER", nullable: false),
                    GenderIdentity = table.Column<int>(type: "INTEGER", nullable: false),
                    HairColor = table.Column<int>(type: "INTEGER", nullable: false),
                    HasChildren = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDrinker = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSmoker = table.Column<bool>(type: "INTEGER", nullable: false),
                    Sexuality = table.Column<int>(type: "INTEGER", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchProfiles_SkinComplexions_ComplexionId",
                        column: x => x.ComplexionId,
                        principalTable: "SkinComplexions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_SeekerId",
                table: "Relationships",
                column: "SeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchProfiles_ComplexionId",
                table: "MatchProfiles",
                column: "ComplexionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchProfiles_UserId",
                table: "MatchProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_AspNetUsers_SeekerId",
                table: "Relationships",
                column: "SeekerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_AspNetUsers_TargetId",
                table: "Relationships",
                column: "TargetId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

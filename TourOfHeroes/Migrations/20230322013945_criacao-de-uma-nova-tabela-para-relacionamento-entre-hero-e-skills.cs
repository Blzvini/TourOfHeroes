using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class criacaodeumanovatabelapararelacionamentoentreheroeskills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Heroes_HeroId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_HeroId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "HeroId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Heroes");

            migrationBuilder.CreateTable(
                name: "HeroSkillsModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeroId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSkillsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroSkillsModel_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroSkillsModel_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroSkillsModel_HeroId",
                table: "HeroSkillsModel",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroSkillsModel_SkillId",
                table: "HeroSkillsModel",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroSkillsModel");

            migrationBuilder.AddColumn<int>(
                name: "HeroId",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Skills",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Heroes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_HeroId",
                table: "Skills",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Heroes_HeroId",
                table: "Skills",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id");
        }
    }
}

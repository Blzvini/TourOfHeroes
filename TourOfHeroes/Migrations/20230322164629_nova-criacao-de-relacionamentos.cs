using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class novacriacaoderelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Heroes_HeroModelId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "HeroSkills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_HeroModelId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "HeroModelId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "HeroModelSkillModel",
                columns: table => new
                {
                    HeroesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroModelSkillModel", x => new { x.HeroesId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_HeroModelSkillModel_Heroes_HeroesId",
                        column: x => x.HeroesId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroModelSkillModel_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroModelSkillModel_SkillsId",
                table: "HeroModelSkillModel",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroModelSkillModel");

            migrationBuilder.AddColumn<int>(
                name: "HeroModelId",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HeroSkills",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSkills", x => new { x.HeroId, x.SkillId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_HeroModelId",
                table: "Skills",
                column: "HeroModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Heroes_HeroModelId",
                table: "Skills",
                column: "HeroModelId",
                principalTable: "Heroes",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaodbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkills_Heroes_HeroId",
                table: "HeroSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkills_Skills_SkillId",
                table: "HeroSkills");

            migrationBuilder.DropIndex(
                name: "IX_HeroSkills_SkillId",
                table: "HeroSkills");

            migrationBuilder.AddColumn<int>(
                name: "HeroModelId",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Heroes_HeroModelId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_HeroModelId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "HeroModelId",
                table: "Skills");

            migrationBuilder.CreateIndex(
                name: "IX_HeroSkills_SkillId",
                table: "HeroSkills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroSkills_Heroes_HeroId",
                table: "HeroSkills",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroSkills_Skills_SkillId",
                table: "HeroSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

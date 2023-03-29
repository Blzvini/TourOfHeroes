using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaolistheroskillmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkills_Skills_SkillId",
                table: "HeroSkills");

            migrationBuilder.DropIndex(
                name: "IX_HeroSkills_SkillId",
                table: "HeroSkills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HeroSkills_SkillId",
                table: "HeroSkills",
                column: "SkillId");

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

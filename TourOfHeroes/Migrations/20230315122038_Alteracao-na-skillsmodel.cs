using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class Alteracaonaskillsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HeroId",
                table: "Skills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_HeroId",
                table: "Skills",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Heroes_HeroId",
                table: "Skills",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

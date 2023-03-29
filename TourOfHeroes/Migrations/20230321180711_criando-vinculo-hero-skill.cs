using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class criandovinculoheroskill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Skills_SkillsId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_SkillsId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "SkillsId",
                table: "Heroes");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Skills",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "HeroId",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Heroes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Skills",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Heroes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillsId",
                table: "Heroes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_SkillsId",
                table: "Heroes",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Skills_SkillsId",
                table: "Heroes",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourOfHeroes.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaonodbcontextadicionandocoisassobreorelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkillsModel_Heroes_HeroId",
                table: "HeroSkillsModel");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkillsModel_Skills_SkillId",
                table: "HeroSkillsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroSkillsModel",
                table: "HeroSkillsModel");

            migrationBuilder.DropIndex(
                name: "IX_HeroSkillsModel_HeroId",
                table: "HeroSkillsModel");

            migrationBuilder.RenameTable(
                name: "HeroSkillsModel",
                newName: "HeroSkills");

            migrationBuilder.RenameIndex(
                name: "IX_HeroSkillsModel_SkillId",
                table: "HeroSkills",
                newName: "IX_HeroSkills_SkillId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HeroSkills",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroSkills",
                table: "HeroSkills",
                columns: new[] { "HeroId", "SkillId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkills_Heroes_HeroId",
                table: "HeroSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroSkills_Skills_SkillId",
                table: "HeroSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroSkills",
                table: "HeroSkills");

            migrationBuilder.RenameTable(
                name: "HeroSkills",
                newName: "HeroSkillsModel");

            migrationBuilder.RenameIndex(
                name: "IX_HeroSkills_SkillId",
                table: "HeroSkillsModel",
                newName: "IX_HeroSkillsModel_SkillId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HeroSkillsModel",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroSkillsModel",
                table: "HeroSkillsModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HeroSkillsModel_HeroId",
                table: "HeroSkillsModel",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroSkillsModel_Heroes_HeroId",
                table: "HeroSkillsModel",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroSkillsModel_Skills_SkillId",
                table: "HeroSkillsModel",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

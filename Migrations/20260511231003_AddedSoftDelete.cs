using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektverwaltung.Migrations
{
    /// <inheritdoc />
    public partial class AddedSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectTask",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Project");
        }
    }
}

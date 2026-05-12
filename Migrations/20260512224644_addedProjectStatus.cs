using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektverwaltung.Migrations
{
    /// <inheritdoc />
    public partial class addedProjectStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Project");
        }
    }
}

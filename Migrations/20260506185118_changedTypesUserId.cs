using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektverwaltung.Migrations
{
    /// <inheritdoc />
    public partial class changedTypesUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_UserId1",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserId1",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Project",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_UserId",
                table: "Project",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_UserId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Project",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId1",
                table: "Project",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_UserId1",
                table: "Project",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

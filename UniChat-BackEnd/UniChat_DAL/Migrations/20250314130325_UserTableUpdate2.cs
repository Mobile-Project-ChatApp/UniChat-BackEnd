using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserTableUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Study",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Study",
                table: "Users");
        }
    }
}

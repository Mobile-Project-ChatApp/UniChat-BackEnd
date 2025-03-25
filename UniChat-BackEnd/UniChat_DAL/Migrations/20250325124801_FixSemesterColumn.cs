using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixSemesterColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the automatic column alteration
            /*
            migrationBuilder.AlterColumn<int>(
                name: "Semester",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
            */
            
            // Use raw SQL with USING clause for safe conversion
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Semester\" TYPE integer USING CASE WHEN \"Semester\" ~ '^[0-9]+$' THEN \"Semester\"::integer ELSE NULL END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Semester",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}

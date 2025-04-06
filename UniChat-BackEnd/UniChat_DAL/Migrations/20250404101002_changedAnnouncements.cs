using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedAnnouncements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnnouncementInteractions",
                table: "UserAnnouncementInteractions");

            migrationBuilder.DropIndex(
                name: "IX_UserAnnouncementInteractions_UserId",
                table: "UserAnnouncementInteractions");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "SenderAvatar",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Announcements",
                newName: "DateCreated");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_Date",
                table: "Announcements",
                newName: "IX_Announcements_DateCreated");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAnnouncementInteractions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Announcements",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnnouncementInteractions",
                table: "UserAnnouncementInteractions",
                columns: new[] { "UserId", "AnnouncementId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnnouncementInteractions",
                table: "UserAnnouncementInteractions");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Announcements",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_DateCreated",
                table: "Announcements",
                newName: "IX_Announcements_Date");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAnnouncementInteractions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Announcements",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Announcements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderAvatar",
                table: "Announcements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnnouncementInteractions",
                table: "UserAnnouncementInteractions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnnouncementInteractions_UserId",
                table: "UserAnnouncementInteractions",
                column: "UserId");
        }
    }
}

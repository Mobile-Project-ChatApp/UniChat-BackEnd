using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class chatroomIdAddedToAnnouncements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatroomId",
                table: "Announcements",
                type: "integer",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_ChatroomId",
                table: "Announcements",
                column: "ChatroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_ChatRooms_ChatroomId",
                table: "Announcements",
                column: "ChatroomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_ChatRooms_ChatroomId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_ChatroomId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "ChatroomId",
                table: "Announcements");
        }
    }
}

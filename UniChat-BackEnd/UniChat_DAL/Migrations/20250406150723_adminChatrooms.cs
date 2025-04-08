using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class adminChatrooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_SenderUserId",
                table: "Announcements");

            migrationBuilder.DropTable(
                name: "UserChatrooms");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_SenderUserId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAnnouncementInteractions");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "Announcements");

            migrationBuilder.CreateTable(
                name: "AdminChatroom",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ChatRoomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminChatroom", x => new { x.UserId, x.ChatRoomId });
                    table.ForeignKey(
                        name: "FK_AdminChatroom_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminChatroom_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_SenderId",
                table: "Announcements",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminChatroom_ChatRoomId",
                table: "AdminChatroom",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_SenderId",
                table: "Announcements",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_SenderId",
                table: "Announcements");

            migrationBuilder.DropTable(
                name: "AdminChatroom");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_SenderId",
                table: "Announcements");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserAnnouncementInteractions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderUserId",
                table: "Announcements",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserChatrooms",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ChatRoomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatrooms", x => new { x.UserId, x.ChatRoomId });
                    table.ForeignKey(
                        name: "FK_UserChatrooms_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatrooms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_SenderUserId",
                table: "Announcements",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatrooms_ChatRoomId",
                table: "UserChatrooms",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_SenderUserId",
                table: "Announcements",
                column: "SenderUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

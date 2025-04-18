using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class inviteLinkUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "InviteLinks");

            migrationBuilder.CreateIndex(
                name: "IX_InviteLinks_ChatroomId",
                table: "InviteLinks",
                column: "ChatroomId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteLinks_CreatedByUserId",
                table: "InviteLinks",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InviteLinks_ChatRooms_ChatroomId",
                table: "InviteLinks",
                column: "ChatroomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InviteLinks_Users_CreatedByUserId",
                table: "InviteLinks",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InviteLinks_ChatRooms_ChatroomId",
                table: "InviteLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_InviteLinks_Users_CreatedByUserId",
                table: "InviteLinks");

            migrationBuilder.DropIndex(
                name: "IX_InviteLinks_ChatroomId",
                table: "InviteLinks");

            migrationBuilder.DropIndex(
                name: "IX_InviteLinks_CreatedByUserId",
                table: "InviteLinks");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "InviteLinks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

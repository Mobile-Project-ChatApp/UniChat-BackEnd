using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniChat_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InvitationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_ChatRooms_RoomId",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Invitations",
                newName: "ChatRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Invitations_RoomId",
                table: "Invitations",
                newName: "IX_Invitations_ChatRoomId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Invitations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_ChatRooms_ChatRoomId",
                table: "Invitations",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_ChatRooms_ChatRoomId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "Invitations",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Invitations_ChatRoomId",
                table: "Invitations",
                newName: "IX_Invitations_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_ChatRooms_RoomId",
                table: "Invitations",
                column: "RoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

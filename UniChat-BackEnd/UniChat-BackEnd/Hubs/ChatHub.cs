using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessageService _messageService;
        private readonly ChatRoomService _chatRoomService;
        private readonly UserService _userService;

        public ChatHub(MessageService messageService, ChatRoomService chatRoomService, UserService userService)
        {
            _messageService = messageService;
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        // TODO: Add user id to the message
        public async Task SendMessage(int roomId, int userId, string message)
        {
            UserDto user = _userService.GetUserById(userId);

            if (user == null)
            {
                await Clients.Caller.SendAsync("UserNotFound", "User not found");
                return;
            }

            MessageDto savedMessage = _messageService.SendMessage(roomId, userId, message);

            if (savedMessage == null)
            {
                await Clients.Caller.SendAsync("ErrorSendingMessage", "Error sending message");
                return;
            }

            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", savedMessage);
        }

        public async Task JoinRoom(int roomId, int userId)
        {
            UserDto user = _userService.GetUserById(userId);

            if (user == null)
            {
                await Clients.Caller.SendAsync("UserNotFound", "User not found");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            
            bool added = _chatRoomService.AddUserToChatRoom(roomId, userId);

            if (!added)
            {
                await Clients.Caller.SendAsync("ErrorAddingUserToChatRoom", "Error adding user to chat room");
                return;
            }
            
            await Clients.Group(roomId.ToString()).SendAsync("UserJoined", user.Username);
        }

        public async Task LeaveRoom(int roomId, int userId)
        {
            UserDto user = _userService.GetUserById(userId);

            if (user == null)
            {
                await Clients.Caller.SendAsync("UserNotFound", "User not found");
                return;
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());

            bool removed = _chatRoomService.RemoveUserFromChatRoom(roomId, userId);

            if (!removed)
            {
                await Clients.Caller.SendAsync("ErrorRemovingUserFromChatRoom", "Error removing user from chat room");
                return;
            }

            await Clients.Group(roomId.ToString()).SendAsync("UserLeft", user.Username);
        }
    }
}

